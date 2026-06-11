using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Xml;

namespace MPL.X4.Parser;

/// <summary>
/// A class that implements the functionality of a data parser.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="serviceProvider">An <see cref="IServiceProvider"/> that is the service provider to use.</param>
internal class DataParser(
                          ILogger<DataParser> logger,
                          IServiceProvider serviceProvider)
    : IDataParser
{
    private IDataParser<TData> GetParserInternal<TData>()
    {
        var returnValue = serviceProvider.GetService<IDataParser<TData>>();
        if (returnValue is null)
        {
            logger.LogWarning("A data parser for {DataType} was not found", typeof(TData));
            throw new InvalidOperationException("A data parser for the specified data type cannot be found");
        }

        return returnValue;
    }

    Task<TData> IDataParser.Parse<TData>(IXDocumentWrapper document)
        => GetParserInternal<TData>().Parse(document);

    Task<TData> IDataParser.Parse<TData>(IXmlReaderWrapper reader)
        => GetParserInternal<TData>().Parse(reader);
}
