using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.Extensions.Logging;

namespace MPL.X4.Services.Xml;

/// <summary>
/// A class that implements a wrapper around an <see cref="XDocument"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="source">An <see cref="XDocument"/> that is the source for the wrapper.</param>
/// <param name="xdocumentWrapperFactory">An <see cref="IXDocumentWrapperFactory"/> that is the XDocument wrapper factory to use.</param>
internal class XDocumentWrapper(
                                ILogger<XDocumentWrapper> logger,
                                XDocument source,
                                IXDocumentWrapperFactory xdocumentWrapperFactory)
    : ParserWrapperBase(logger),
      IXDocumentWrapper
{
    private bool TryGetAttributeInternal<T>(string xpathExpression, string attributeName, TryParseDelegate<T> parser, [NotNullWhen(true)] out T? value)
        where T : struct
    {
        value = null;

        if (((IXDocumentWrapper)this).TryGetAttribute(xpathExpression, attributeName, out string? stringValue) &&
            parser(stringValue, out T parsed))
        {
            value = parsed;
            return true;
        }

        return false;
    }

    XElement IXDocumentWrapper.SelectElement(string xpathExpression)
    {
        if (!((IXDocumentWrapper)this).TrySelectElement(xpathExpression, out var returnValue))
        {
            logger.LogWarning("The XPath expression {XPathExpression} did not return an element", xpathExpression);
            throw new ArgumentException("The specified XPath expression did not return an element", nameof(xpathExpression));
        }

        return returnValue;
    }

    IXDocumentWrapper IXDocumentWrapper.SelectElementAsDocument(string xpathExpression)
    {
        var element = ((IXDocumentWrapper)this).SelectElement(xpathExpression);

        return xdocumentWrapperFactory.CreateInstance(element);
    }

    IEnumerable<XElement> IXDocumentWrapper.SelectElements(string xpathExpression)
        => source.XPathSelectElements(xpathExpression);

    IEnumerable<IXDocumentWrapper> IXDocumentWrapper.SelectElementsAsDocument(string xpathExpression)
        => ((IXDocumentWrapper)this)
                                    .SelectElements(xpathExpression)
                                    .Select(x => xdocumentWrapperFactory.CreateInstance(x));
    
    bool IXDocumentWrapper.TryGetAttribute(string xpathExpression, string attributeName, [NotNullWhen(true)] out decimal? value)
        => TryGetAttributeInternal(xpathExpression, attributeName, decimal.TryParse, out value);

    bool IXDocumentWrapper.TryGetAttribute(string xpathExpression, string attributeName, [NotNullWhen(true)] out double? value)
        => TryGetAttributeInternal(xpathExpression, attributeName, double.TryParse, out value);

    bool IXDocumentWrapper.TryGetAttribute(string xpathExpression, string attributeName, [NotNullWhen(true)] out int? value)
        => TryGetAttributeInternal(xpathExpression, attributeName, int.TryParse, out value);

    bool IXDocumentWrapper.TryGetAttribute(string xpathExpression, string attributeName, [NotNullWhen(true)] out string? value)
    {
        ((IXDocumentWrapper)this).TrySelectElement(xpathExpression, out var node);
        value = node?.Attribute(attributeName)?.Value;

        return value is not null;
    }

    bool IXDocumentWrapper.TryGetRootAttribute(string attributeName, [NotNullWhen(true)] out decimal? value)
        => ((IXDocumentWrapper)this).TryGetAttribute(Constants.XmlDataFile.XPath.RootElement, attributeName, out value);

    bool IXDocumentWrapper.TryGetRootAttribute(string attributeName, [NotNullWhen(true)] out double? value)
      => ((IXDocumentWrapper)this).TryGetAttribute(Constants.XmlDataFile.XPath.RootElement, attributeName, out value);

    bool IXDocumentWrapper.TryGetRootAttribute(string attributeName, [NotNullWhen(true)] out int? value)
        => ((IXDocumentWrapper)this).TryGetAttribute(Constants.XmlDataFile.XPath.RootElement, attributeName, out value);

    bool IXDocumentWrapper.TryGetRootAttribute(string attributeName, [NotNullWhen(true)] out string? value)
        => ((IXDocumentWrapper)this).TryGetAttribute(Constants.XmlDataFile.XPath.RootElement, attributeName, out value);

    bool IXDocumentWrapper.TrySelectElement(string xpathExpression, [NotNullWhen(true)] out XElement? element)
    {
        element = source.XPathSelectElement(xpathExpression);
        return element is not null;
    }

    bool IXDocumentWrapper.TrySelectElementAsDocument(string xpathExpression, [NotNullWhen(true)] out IXDocumentWrapper? element)
    {
        element = ((IXDocumentWrapper)this).TrySelectElement(xpathExpression, out var foundElement)
            ? xdocumentWrapperFactory.CreateInstance(foundElement)
            : null;

        return element is not null;
    }
}
