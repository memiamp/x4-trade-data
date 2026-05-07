namespace MPL.X4.GameResources.Data.Services;

/// <summary>
/// An interface that defines the behaviour of a game resource data parser.
/// </summary>
public interface IGameResourceDataParser
{
    /// <summary>
    /// Parses colour resource data from the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> containing the resource data to parse.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IColourResourceData"/> containing the result.</returns>
    Task<IColourResourceData> ReadColourResources(IXmlReaderWrapper reader);

    /// <summary>
    /// Parses factions data from the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> containing the resource data to parse.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IFactionDataDictionary"/> containing the result.</returns>
    Task<IFactionDataDictionary> ReadFactions(IXmlReaderWrapper reader);

    /// <summary>
    /// Parses the sector macro map from the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> containing the resource data to parse.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="Dictionary{TKey, TValue}"/> containing the result.</returns>
    Task<Dictionary<string, string>> ReadSectorMacroMap(IXmlReaderWrapper reader);

    /// <summary>
    /// Reads text resource data from the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> containing the resource data to parse.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  A <see cref="ITextResourcePageDictionary"/> containing the result.</returns>
    Task<ITextResourcePageDictionary> ReadTextResources(IXmlReaderWrapper reader);

    /// <summary>
    /// Reads zone offset data from the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> containing the resource data to parse.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  A <see cref="IZoneOffsetDataDictionary"/> containing the result.</returns>
    Task<IZoneOffsetDataDictionary> ReadZoneOffsets(IXmlReaderWrapper reader);
}
