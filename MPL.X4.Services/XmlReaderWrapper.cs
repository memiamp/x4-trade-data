using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.Extensions.Logging;

namespace MPL.X4.Services;

/// <summary>
/// A class that implements a wrapper around a <see cref="XmlReader"/>.
/// </summary>
/// <param name="xmlReader">An <see cref="XmlReader"/> that is the Xml Reader being wrapped.</param>
/// <param name="xmlReaderFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XmlReader factory to use.</param>
internal sealed class XmlReaderWrapper(
                                       XmlReader xmlReader,
                                       IXmlReaderWrapperFactory xmlReaderFactory)
    : IXmlReaderWrapper
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

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    bool IXmlReaderWrapper.CheckNodeMatches(string name, XmlNodeType nodeType)
        => xmlReader.NodeType == nodeType &&
           xmlReader.Name == name;

    string? IXmlReaderWrapper.GetAttribute(string name)
        => xmlReader.GetAttribute(name);

    Task<bool> IXmlReaderWrapper.ReadAsync()
        => xmlReader.ReadAsync();

    Task<string> IXmlReaderWrapper.ReadElementContentAsStringAsync()
        => xmlReader.ReadElementContentAsStringAsync();

    Task<string> IXmlReaderWrapper.ReadInnerXmlAsync()
        => xmlReader.ReadInnerXmlAsync();

    IXmlReaderWrapper IXmlReaderWrapper.ReadSubtree()
    {
        // The caller is expected to dispose this once consumed
        var subtree = xmlReader.ReadSubtree();
        return xmlReaderFactory.CreateXmlReader(subtree);
    }

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

    bool IXmlReaderWrapper.TryGetAttribute(string name, [NotNullWhen(true)] out int? value)
    {
        value = null;

        var attributeValue = xmlReader.GetAttribute(name);
        if (int.TryParse(attributeValue, out var outValue))
        {
            value = outValue;
        }

        return value is not null;
    }

    bool IXmlReaderWrapper.TryGetAttribute(string name, [NotNullWhen(true)] out string? value)
    {
        value = xmlReader.GetAttribute(name);

        return value is not null;
    }

    string IXmlReaderWrapper.Name => xmlReader.Name;

    XmlNodeType IXmlReaderWrapper.NodeType => xmlReader.NodeType;
}
