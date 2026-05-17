// Code taken from https://github.com/chemodun/X4-XMLDiffAndPatch
// Original code was released under APACHE 2.0 License - "see License\Apache 2.0 License.txt"
// Original code Copyright (c) chemodun
// Modified by Martin Parkin in May 2026.
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.Extensions.Logging;

namespace MPL.X4.Imports.XmlPatch;

/// <summary>
/// A class that implements a patching engine for X4 XML data.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class PatchEngine(
                           ILogger<PatchEngine> logger)
    : IPatchEngine
{
    private const string AddOperationAfter = "after";
    private const string AddOperationAppend = "append";
    private const string AddOperationBefore = "before";
    private const string AddOperationPrepend = "prepend";
    
    private const string AttributeIf = "if";
    private const string AttributePos = "pos";
    private const string AttributeSel = "sel";
    private const string AttributeType = "type";

    private enum AddOperationPosition
    {
        Unknown = 0,
        After,
        Append,
        Attribute,
        Before,
        Prepend
    }

    private void AddInternal(XElement change, XElement target, bool allowDoubles, AddOperationPosition position)
    {
        XNode? latestAdded = null;

        var nodes = change
                          .Nodes()
                          .Where(x => x is XElement or XComment)
                          .Select(x => x is XElement ne
                                                        ? (XNode)new XElement(ne)
                                                        : new XComment(((XComment)x).Value));
        foreach (var cloned in nodes)
        {
            if (latestAdded is null)
            {
                // First node: apply duplicate check for elements, then insert at position
                if (!allowDoubles &&
                    cloned is XElement clonedElem)
                {
                    // Duplicate check: both-direction attribute equality
                    var searchIn = position is AddOperationPosition.Before or AddOperationPosition.After
                        ? target.Parent!.Elements()
                        : target.Elements();

                    var isDuplicate = searchIn.Any(e =>
                        e.Name == clonedElem.Name &&
                        e.Attributes().All(a => clonedElem.Attribute(a.Name)?.Value == a.Value) &&
                        clonedElem.Attributes().All(a => e.Attribute(a.Name)?.Value == a.Value)
                    );

                    if (isDuplicate)
                    {
                        logger.LogWarning("Duplicate element already exists: '{ElementInfo}'", GetElementInfo(target));
                        continue;
                    }
                }

                switch (position)
                {
                    case AddOperationPosition.After:
                        target.AddAfterSelf(cloned);
                        logger.LogDebug("Inserted element after '{ElementInfo}'", GetElementInfo(target));
                        break;

                    case AddOperationPosition.Append:
                        target.Add(cloned);
                        logger.LogDebug("Appended element to '{ElementInfo}'", GetElementInfo(target));
                        break;

                    case AddOperationPosition.Before:
                        target.AddBeforeSelf(cloned);
                        logger.LogDebug("Inserted element before '{ElementInfo}'", GetElementInfo(target));
                        break;

                    case AddOperationPosition.Prepend:
                        target.AddFirst(cloned);
                        logger.LogDebug("Prepended element to '{ElementInfo}'", GetElementInfo(target));
                        break;
                }

                latestAdded = cloned;
            }
            else
            {
                // Subsequent nodes: always AddAfterSelf to preserve insertion order
                latestAdded.AddAfterSelf(cloned);
                latestAdded = cloned;
                logger.LogDebug("Added subsequent node after previous");
            }
        }
    }

    private static string GetElementInfo(XElement? element)
    {
        if (element == null)
            return "<null>";
        var sb = new System.Text.StringBuilder("<");
        sb.Append(element.Name.LocalName);
        var first = element.FirstAttribute;
        if (first != null)
        {
            sb.Append($" {first.Name.LocalName}=\"{first.Value}\"");
            if (element.Attributes().Count() > 1)
                sb.Append(" ...");
        }
        sb.Append('>');
        return sb.ToString();
    }

    private static bool GetIfCheckResult(XElement originalRoot, string? ifCheck)
    {
        if (ifCheck is null)
            return true;

        return originalRoot.XPathEvaluate(ifCheck) switch
        {
            bool returnValue => returnValue,
            IEnumerable enumerable => enumerable.OfType<object>().Any(),
            _ => throw new ArgumentException($"The specified if check operation '{ifCheck}' returned an unknown result", nameof(ifCheck))
        };
    }

    private static string LastApplicableNode(string selector, XElement root)
    {
        var parts = selector.Split('/');
        var current = "";
        var last = "";

        foreach (var part in parts)
        {
            if (string.IsNullOrEmpty(part))
            {
                current += "/";
                continue;
            }
            current += (current.EndsWith('/') ? "" : "/") + part;

            try
            {
                var matches = root.XPathEvaluate(current) as IEnumerable<object>;
                if (matches?.Any() == true)
                    last = current;
                else
                    break;
            }
            catch
            {
                break;
            }
        }

        return last;
    }

    private static AddOperationPosition ParseAddOperationPosition(string? positionText, string? typeText)
        => (positionText, typeText) switch
        {
            (null, null) => AddOperationPosition.Append,
            (AddOperationAfter, _) => AddOperationPosition.After,
            (AddOperationAppend, _) => AddOperationPosition.Append,
            (AddOperationBefore, _) => AddOperationPosition.Before,
            (AddOperationPrepend, _) => AddOperationPosition.Prepend,
            (_, not null) when typeText.StartsWith('@') => AddOperationPosition.Attribute,
            _ => AddOperationPosition.Unknown
        };

    private bool TryGetAddOperationElements(XElement originalRoot, XElement change, out AddOperationPosition position, [NotNullWhen(true)] out XElement? target, out string? attributeName, out string? ifCheck)
    {
        attributeName = null;
        ifCheck = null;
        position = AddOperationPosition.Unknown;
        var returnValue = false;
        target = null;

        var sel = change.Attribute(AttributeSel)?.Value;
        if (sel is not null)
        {
            var pos = change.Attribute(AttributePos)?.Value;
            var type = change.Attribute(AttributeType)?.Value;

            ifCheck = change.Attribute(AttributeIf)?.Value;
            position = ParseAddOperationPosition(pos, type);

            logger.LogDebug("Add operation sel='{SelValue}' pos='{PosValue}' type='{TypeValue}'", sel, pos, type);

            if (position == AddOperationPosition.Attribute)
            {
                if (type?.Length > 1 &&
                    type.StartsWith('@'))
                {
                    attributeName = type[1..];
                }
                else
                {
                    logger.LogWarning("Add operation has invalid type ({TypeValue}) for attribute operation", type);
                }
            }
            else if (type?.Length > 0)
            {
                logger.LogWarning("Add operation has invalid type ({TypeValue}) for non-attribute operation", type);
                return false;
            }

            var targets = originalRoot.XPathSelectElements(sel);
            if (targets.Any())
            {
                if (targets.Count() == 1)
                {
                    target = targets.First();
                    returnValue = true;
                }
                else
                {
                    logger.LogWarning("Add operation has multiple targets ({TargetCount}) found for sel='{SelValue}'", targets.Count(), sel);
                }
            }
            else
            {
                logger.LogWarning("Add operation no element found for sel='{SelValue}'. Last resolvable: '{LastResolvable}'", sel, LastApplicableNode(sel, originalRoot));
            }
        }
        else
        {
            logger.LogWarning("Replace operation missing '{AttributeName}' attribute", AttributeSel);
        }

        return returnValue;
    }

    private bool TryGetTargetList(XElement originalRoot, XElement change, string operation, out List<object> targets)
    {
        var returnValue = false;
        targets = [];

        var sel = change.Attribute(AttributeSel)?.Value;
        if (sel == null)
        {
            logger.LogWarning("{OperationName} operation missing '{AttributeName}' attribute", operation, AttributeSel);
        }
        else
        {
            logger.LogDebug("{OperationName} sel='{SelValue}'", operation, sel);

            var results = (originalRoot.XPathEvaluate(sel) as IEnumerable<object>);
            if (results?.Any() == true)
            {
                targets = [.. results];
                returnValue = true;
            }
            else
            {
                logger.LogWarning("No nodes found to {OperationName} for sel='{SelValue}'. Last resolvable: '{LastResolvable}'", operation, sel, LastApplicableNode(sel, originalRoot));
            }
        }

        return returnValue;
    }


    void IPatchEngine.Add(XElement originalRoot, XElement change, bool allowDoubles)
    {
        if (!TryGetAddOperationElements(originalRoot, change, out var position, out var target, out var attributeName, out var ifCheck))
        {
            return;
        }

        if (GetIfCheckResult(originalRoot, ifCheck))
        {
            if (position == AddOperationPosition.Attribute)
            {
                target.SetAttributeValue(attributeName!, change.Value);
                logger.LogDebug("Added attribute '{AttributeName}' with '{AttributeValue'", GetElementInfo(target));
            }
            else if (position == AddOperationPosition.Unknown)
            {
                logger.LogWarning("Cannot perform add operation due to invalid position");
            }
            else
            {
                AddInternal(change, target, allowDoubles, position);
            }
        }
    }

    void IPatchEngine.Remove(XElement originalRoot, XElement change)
    {
        if (GetIfCheckResult(originalRoot, change.Attribute(AttributeIf)?.Value))
        {
            if (TryGetTargetList(originalRoot, change, nameof(IPatchEngine.Remove), out var results))
            {
                foreach (var result in results)
                {
                    switch (result)
                    {
                        case XElement element when element.Parent is not null:
                            element.Remove();
                            logger.LogDebug("Removed element '{ElementInfo}'", GetElementInfo(element));
                            break;

                        case XAttribute attr when attr.Parent is not null:
                            attr.Remove();
                            logger.LogDebug("Removed attribute '{AttributeName}'", attr.Name);
                            break;

                        case XText textNode:
                            textNode.Remove();
                            logger.LogDebug("Removed text node");
                            break;

                        default:
                            logger.LogWarning("Cannot remove node type '{NodeType}'", result?.GetType().Name);
                            break;
                    }
                }
            }
        }
    }

    void IPatchEngine.Replace(XElement originalRoot, XElement change)
    {
        if (GetIfCheckResult(originalRoot, change.Attribute(AttributeIf)?.Value))
        {
            if (TryGetTargetList(originalRoot, change, nameof(IPatchEngine.Replace), out var results))
            {
                foreach (var result in results)
                {
                    switch (result)
                    {
                        case XElement target:
                            var replaceContent = change
                                                       .Elements()
                                                       .Select(e => new XElement(e));
                            if (replaceContent.Any())
                            {
                                target.ReplaceWith([.. replaceContent.Cast<object>()]);
                                logger.LogDebug("Replacement replaced {Target} with {Length} element(s)", GetElementInfo(target), replaceContent.Count());
                            }
                            else
                            {
                                logger.LogWarning("No child elements to replace");
                            }
                            break;

                        case XText textNode:
                            textNode.Value = change.Value;
                            logger.LogDebug("Replacement set text node value to '{NewValue}'", change.Value);
                            break;

                        case XAttribute attr:
                            attr.Value = change.Value;
                            logger.LogDebug("Replacement set attribute '{AttributeName}' = '{NewValue}'", attr.Name, change.Value);
                            break;

                        default:
                            logger.LogWarning("Unsupported node type for replacement '{NodeType}'", result?.GetType().Name);
                            break;
                    }
                }
            }
        }
    }
}
