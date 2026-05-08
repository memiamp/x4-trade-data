using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace MPL.X4.Services.Xml;

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

    bool IXmlReaderWrapper.CheckNodeMatches(string name, XmlNodeType nodeType, int depth)
        => ((IXmlReaderWrapper)this).CheckNodeMatches(name, nodeType) &&
           xmlReader.Depth == depth;

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

    bool IXmlReaderWrapper.TryGetAttribute(string name, [NotNullWhen(true)] out double? value)
    {
        value = null;

        var attributeValue = xmlReader.GetAttribute(name);
        if (double.TryParse(attributeValue, out var outValue))
        {
            value = outValue;
        }

        return value is not null;
    }

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

    int IXmlReaderWrapper.Depth => xmlReader.Depth;

    string IXmlReaderWrapper.Name => xmlReader.Name;

    XmlNodeType IXmlReaderWrapper.NodeType => xmlReader.NodeType;
}
