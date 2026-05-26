using System.Xml.Linq;

namespace MPL.X4.Services.Xml;

/// <summary>
/// A class that implements a factory for <see cref="IXmlReaderWrapper"/> instances.
/// </summary>
internal class XDocumentWrapperFactory : IXDocumentWrapperFactory
{
    async Task<IXDocumentWrapper> IXDocumentWrapperFactory.CreateInstance(IXmlReaderWrapper reader)
    {
        var document = await XDocument.LoadAsync(reader.SourceReader, LoadOptions.None, CancellationToken.None);

        return new XDocumentWrapper(document);
    }
}
