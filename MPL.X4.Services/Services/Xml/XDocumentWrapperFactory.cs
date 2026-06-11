using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace MPL.X4.Services.Xml;

/// <summary>
/// A class that implements a factory for <see cref="IXmlReaderWrapper"/> instances.
/// </summary>
/// <param name="serviceProvider">An <see cref="IServiceProvider"/> that is the service provider to use to create the instance.</param>
internal class XDocumentWrapperFactory(
                                       IServiceProvider serviceProvider)
    : IXDocumentWrapperFactory
{
    async Task<IXDocumentWrapper> IXDocumentWrapperFactory.CreateInstance(IXmlReaderWrapper reader)
    {
        var document = await XDocument.LoadAsync(reader.SourceReader, LoadOptions.None, CancellationToken.None);

        return ActivatorUtilities.CreateInstance<XDocumentWrapper>(serviceProvider, document);
    }

    IXDocumentWrapper IXDocumentWrapperFactory.CreateInstance(XElement element)
    {
        var document = new XDocument(element);

        return ActivatorUtilities.CreateInstance<XDocumentWrapper>(serviceProvider, document);
    }
}
