// Code taken from https://github.com/chemodun/X4-XMLDiffAndPatch
// Original code was released under APACHE 2.0 License - "see License\Apache 2.0 License.txt"
// Original code Copyright (c) chemodun
// Modified by Martin Parkin in May 2026.
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;

namespace MPL.X4.Imports.XmlPatch;

/// <summary>
/// A class that implements a patching service for X4 XML.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="patchEngine">An <see cref="IPatchEngine"/> that is the XML patching engine to use.</param>
internal class XmlPatchService(
                               ILogger<XmlPatchService> logger,
                               IPatchEngine patchEngine)
    : IXmlPatchService
{
    private const string ElementDiff = "diff";
    private const string OperationAdd = "add";
    private const string OperationRemove = "remove";
    private const string OperationReplace = "replace";

    private bool VerifyParameters(
                                  XDocument source,
                                  XDocument difference,
                                  [NotNullWhen(true)] out XElement? sourceRoot,
                                  [NotNullWhen(true)] out XElement? differenceRoot)
    {
        differenceRoot = null;
        sourceRoot = null;

        if (source.Root is null)
        {
            logger.LogWarning("The source document has no elements");
            return false;
        }

        if (difference.Root is null)
        {
            logger.LogWarning("The difference document has no elements");
            return false;
        }

        if (difference.Root.Name.LocalName != ElementDiff)
        {
            logger.LogWarning("Root element of difference document is not '{DiffElement}'. Found: '{RootName}'", ElementDiff, difference.Root.Name);
            return false;
        }

        if (!difference.Root.HasElements == true)
        {
            logger.LogInformation("Difference document has no operations");
            return false;
        }

        sourceRoot = source.Root;
        differenceRoot = difference.Root;

        return true;
    }

    XDocument IXmlPatchService.Patch(string sourceXml, string differenceXml)
    {
        var source = XDocument.Parse(sourceXml);

        return ((IXmlPatchService)this).Patch(source, differenceXml);
    }

    XDocument IXmlPatchService.Patch(XDocument source, string differenceXml)
    {
        var difference = XDocument.Parse(differenceXml, LoadOptions.SetLineInfo);

        return ((IXmlPatchService)this).Patch(source, difference);
    }

    XDocument IXmlPatchService.Patch(XDocument source, XDocument difference)
    {
        if (!VerifyParameters(source, difference, out var sourceRoot, out var differenceRoot))
        {
            logger.LogWarning("Could not patch source. See log for detail");
            return source;
        }

        foreach (var node in differenceRoot.Nodes())
        {
            if (node is not XElement operation)
            {
                logger.LogDebug("Skipping non-element node in difference: {NodeType}", node.NodeType);
                continue;
            }

            switch (operation.Name.LocalName)
            {
                case OperationAdd:
                    patchEngine.Add(sourceRoot, operation, false);
                    break;

                case OperationRemove:
                    patchEngine.Remove(sourceRoot, operation);
                    break;

                case OperationReplace:
                    patchEngine.Replace(sourceRoot, operation);
                    break;

                default:
                    logger.LogDebug("Unknown difference operation '{OperationName}'", operation.Name.LocalName);
                    break;
            }
        }

        return source;
    }

    string IXmlPatchService.PatchToString(string sourceXml, string differenceXml)
        => ((IXmlPatchService)this).Patch(sourceXml, differenceXml).ToString();
}