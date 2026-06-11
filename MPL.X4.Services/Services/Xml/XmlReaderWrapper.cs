using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.Extensions.Logging;

namespace MPL.X4.Services.Xml;

/// <summary>
/// A class that implements a wrapper around a <see cref="XmlReader"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="xdocumentWrapperFactory">An <see cref="IXDocumentWrapperFactory"/> that is the XDocument wrapper factory to use.</param>
/// <param name="xmlReader">An <see cref="XmlReader"/> that is the Xml Reader being wrapped.</param>
/// <param name="xmlReaderFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XmlReader wrapper factory to use.</param>
internal sealed class XmlReaderWrapper(
                                       ILogger<XDocumentWrapper> logger,
                                       IXDocumentWrapperFactory xdocumentWrapperFactory,
                                       XmlReader xmlReader,
                                       IXmlReaderWrapperFactory xmlReaderFactory)
    : ParserWrapperBase(logger),
      IXmlReaderWrapper
{
    private bool _disposedValue;

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                xmlReader.Dispose();
            }

            _disposedValue = true;
        }
    }

    private bool TryGetAttributeInternal<T>(string name, TryParseDelegate<T> parser, [NotNullWhen(true)] out T? value)
        where T : struct
    {
        value = null;

        var attributeValue = xmlReader.GetAttribute(name);

        if (parser(attributeValue, out T parsed))
        {
            value = parsed;
            return true;
        }

        return false;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    bool IXmlReaderWrapper.CheckNodeMatches(string name, XmlNodeType nodeType)
        => xmlReader.NodeType == nodeType &&
           xmlReader.Name == name;

    bool IXmlReaderWrapper.CheckNodeMatches(string name, XmlNodeType nodeType, int depth)
        => xmlReader.Depth == depth &&
           ((IXmlReaderWrapper)this).CheckNodeMatches(name, nodeType);

    string? IXmlReaderWrapper.GetAttribute(string name)
        => xmlReader.GetAttribute(name);

    Task<bool> IXmlReaderWrapper.ReadAsync()
        => xmlReader.ReadAsync();

    Task<string> IXmlReaderWrapper.ReadElementContentAsStringAsync()
        => xmlReader.ReadElementContentAsStringAsync();

    Task<string> IXmlReaderWrapper.ReadInnerXmlAsync()
        => xmlReader.ReadInnerXmlAsync();

    async Task<IXmlReaderWrapper> IXmlReaderWrapper.ReadSubtree(bool moveToFirstElement)
    {
        // The caller is expected to dispose this once consumed
        var subtree = xmlReader.ReadSubtree();
        var returnValue = xmlReaderFactory.CreateXmlReader(subtree);

        if (moveToFirstElement)
        {
            await returnValue.ReadAsync();
        }

        return returnValue;
    }

    Task<IXDocumentWrapper> IXmlReaderWrapper.ReadSubtreeToXDocument()
    {
        // The caller is expected to dispose this once consumed
        var subtree = xmlReader.ReadSubtree();
        var reader = xmlReaderFactory.CreateXmlReader(subtree);
        return reader.ToXDocument();
    }

    Task<IXDocumentWrapper> IXmlReaderWrapper.ToXDocument()
        => xdocumentWrapperFactory.CreateInstance(this);

    bool IXmlReaderWrapper.TryGetAttribute(string name, Func<string, bool> predicate, [NotNullWhen(true)] out string? value)
    {
        value = null;

        var attributeValue = xmlReader.GetAttribute(name);
        if (!string.IsNullOrWhiteSpace(attributeValue) &&
            predicate(attributeValue))
        {
            value = attributeValue;
        }

        return value is not null;
    }

    bool IXmlReaderWrapper.TryGetAttribute(string name, Func<string, bool> predicate)
        => ((IXmlReaderWrapper)this).TryGetAttribute(name, predicate, out _);

    bool IXmlReaderWrapper.TryGetAttribute(string name, [NotNullWhen(true)] out decimal? value)
        => TryGetAttributeInternal(name, decimal.TryParse, out value);

    bool IXmlReaderWrapper.TryGetAttribute(string name, [NotNullWhen(true)] out double? value)
        => TryGetAttributeInternal(name, double.TryParse, out value);

    bool IXmlReaderWrapper.TryGetAttribute(string name, [NotNullWhen(true)] out int? value)
        => TryGetAttributeInternal(name, int.TryParse, out value);

    bool IXmlReaderWrapper.TryGetAttribute(string name, [NotNullWhen(true)] out long? value)
        => TryGetAttributeInternal(name, long.TryParse, out value);

    bool IXmlReaderWrapper.TryGetAttribute(string name, [NotNullWhen(true)] out string? value)
    {
        value = xmlReader.GetAttribute(name);

        return value is not null;
    }

    int IXmlReaderWrapper.Depth => xmlReader.Depth;

    string IXmlReaderWrapper.Name => xmlReader.Name;

    XmlNodeType IXmlReaderWrapper.NodeType => xmlReader.NodeType;

    XmlReader IXmlReaderWrapper.SourceReader => xmlReader;
}
