using System.IO.Compression;
using System.Text;
using System.Xml;
using Microsoft.Extensions.DependencyInjection;

namespace MPL.X4.Services;

/// <summary>
/// A class that implements a factory for <see cref="IXmlReaderWrapper"/> instances.
/// </summary>
/// <param name="serviceProvider">An <see cref="IServiceProvider"/> that is the service provider to use to create the instance.</param>
internal class XmlReaderWrapperFactory(
                                       IServiceProvider serviceProvider)
    : IXmlReaderWrapperFactory
{
    private static XmlReaderSettings GetDefaultSettings()
        => new()
        {
            Async = true,
            CloseInput = true,
            DtdProcessing = DtdProcessing.Ignore,
            IgnoreWhitespace = true
        };

    private IXmlReaderWrapper CreateXmlReaderInternal(string sourcePath, XmlReaderSettings? settings = null)
    {
        XmlReader? reader = null;

        var extension = Path.GetExtension(sourcePath);
        if (extension.Equals(".gz", StringComparison.OrdinalIgnoreCase))
        {
            var fileStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress);
            reader = XmlReader.Create(gzipStream, settings ?? GetDefaultSettings());
        }
        else
        {
            var readerStream = new StreamReader(sourcePath, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
            reader = XmlReader.Create(readerStream, settings ?? GetDefaultSettings());
        }

        return CreateXmlReaderInternal(reader);
    }

    private IXmlReaderWrapper CreateXmlReaderInternal(XmlReader reader)
        => ActivatorUtilities.CreateInstance<XmlReaderWrapper>(serviceProvider, reader);

    IXmlReaderWrapper IXmlReaderWrapperFactory.CreateXmlReader(string inputUri, XmlReaderSettings settings)
      => CreateXmlReaderInternal(inputUri, settings);

    IXmlReaderWrapper IXmlReaderWrapperFactory.CreateXmlReader(string inputUri)
        => CreateXmlReaderInternal(inputUri);

    IXmlReaderWrapper IXmlReaderWrapperFactory.CreateXmlReader(XmlReader source)
        => CreateXmlReaderInternal(source);
}
