using Microsoft.Extensions.Logging;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements the base functionality of a data parser.
/// </summary>
/// <typeparam name="TData">The type of the data to be parsed.</typeparam>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal abstract class DataParserBase<TData>(
                                              ILogger<DataParserBase<TData>> logger)
    : IDataParser<TData>
{
    /// <summary>
    /// Gets an indication of whether the item being loaded is known to the player.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> that is the source reader.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    private protected static bool GetIsKnownToPlayer(IXmlReaderWrapper reader)
        => reader.TryGetAttribute(
                                  Constants.SaveGameFile.AttributeName.KnownTo,
                                  x => x == Constants.SaveGameFile.AttributeValue.KnownTo.Player);

    /// <summary>
    /// Invoked to parse data from the specified <paramref name="reader"/>, using the specified <paramref name="positionOffset"/> to adjust any position data as needed.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> that is the data reader.</param>
    /// <param name="positionOffset">An <see cref="ISectorPosition"/> that is the position offset.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. A <typeparamref name="TData"/> that is the result.</returns>
    private protected abstract Task<TData> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset);

    /// <summary>
    /// Gets the logger.
    /// </summary>
    private protected ILogger Logger => logger;

    Task<TData> IDataParser<TData>.Parse(IXmlReaderWrapper reader)
        => ((IDataParser<TData>)this).Parse(reader, ISectorPosition.GetDefault());

    Task<TData> IDataParser<TData>.Parse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
        => OnParse(reader, positionOffset);
}
