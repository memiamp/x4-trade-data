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
    Task<TData> IDataParser.Parse<TData>(IXmlReaderWrapper reader)
    {
        var parser = serviceProvider.GetService<IDataParser<TData>>();
        if (parser is null)
        {
            logger.LogWarning("A data parser for {DataType} was not found", typeof(TData));
            throw new InvalidOperationException("A data parser for the specified data type cannot be found");
        }

        return parser.Parse(reader);
    }
}
