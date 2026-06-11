using Microsoft.Extensions.Logging;
using MPL.X4.Services.Xml;

namespace MPL.X4.Parser;

/// <summary>
/// A class that implements the base functionality of a data parser.
/// </summary>
/// <typeparam name="TData">The type of the data to be parsed.</typeparam>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal abstract class DataParserBase<TData>(
                                              IDataParser dataParser,
                                              ILogger<DataParserBase<TData>> logger)
    : IDataParser<TData>
{
    /// <summary>
    /// Gets an indication of whether the item being loaded is known to the player.
    /// </summary>
    /// <param name="document">An <see cref="IXDocumentWrapper"/> that is the source document.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    private protected static bool GetIsKnownToPlayer(IXDocumentWrapper document)
        => document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.KnownTo, out string? value) &&
           value == Constants.XmlDataFile.AttributeValue.KnownTo.Player;

    /// <summary>
    /// Gets an indication of whether the item being loaded is known to the player.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> that is the source reader.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    private protected static bool GetIsKnownToPlayer(IXmlReaderWrapper reader)
        => reader.TryGetAttribute(
                                  Constants.XmlDataFile.AttributeName.KnownTo,
                                  x => x == Constants.XmlDataFile.AttributeValue.KnownTo.Player);

    /// <summary>
    /// Invoked to parse data from the specified <paramref name="document"/>.
    /// </summary>
    /// <param name="document">An <see cref="IXmlReaderWrapper"/> that is the data reader.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. A <typeparamref name="TData"/> that is the result.</returns>
    private protected virtual Task<TData> OnParse(IXDocumentWrapper document)
    {
        logger.LogWarning("Call to parser to process document but document parsing has not been implemented");
        throw new NotImplementedException("This parser does not have a document parsing implementation");
    }

    /// <summary>
    /// Invoked to parse data from the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> that is the data reader.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. A <typeparamref name="TData"/> that is the result.</returns>
    private protected virtual Task<TData> OnParse(IXmlReaderWrapper reader)
    {
        logger.LogWarning("Call to parser to process reader but reader parsing has not been implemented");
        throw new NotImplementedException("This parser does not have a reader parsing implementation");
    }

    /// <summary>
    /// Gets the data parser.
    /// </summary>
    private protected IDataParser DataParser => dataParser;

    /// <summary>
    /// Gets the logger.
    /// </summary>
    private protected ILogger Logger => logger;

    Task<TData> IDataParser<TData>.Parse(IXDocumentWrapper document)
       => OnParse(document);

    Task<TData> IDataParser<TData>.Parse(IXmlReaderWrapper reader)
       => OnParse(reader);
}
