using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MPL.X4.Services.Xml;

/// <summary>
/// A class that implements a wrapper around an <see cref="XDocument"/>.
/// </summary>
/// <param name="source">An <see cref="XDocument"/> that is the source for the wrapper.</param>
internal class XDocumentWrapper(
                                XDocument source)
    : IXDocumentWrapper
{
    bool IXDocumentWrapper.TryGetAttribute(string xpathExpression, string attributeName, [NotNullWhen(true)] out string? value)
    {
        var node = ((IXDocumentWrapper)this).XPathSelectElement(xpathExpression);
        value = node?.Attribute(attributeName)?.Value;

        return value is not null;
    }

    XElement? IXDocumentWrapper.XPathSelectElement(string expression)
        => source.XPathSelectElement(expression);

}
