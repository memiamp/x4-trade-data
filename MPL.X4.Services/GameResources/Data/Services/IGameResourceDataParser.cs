using MPL.X4.Services.Xml;

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
    /// Parses the landmark names from the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> containing the resource data to parse.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IMacroNameResourceDataDictionary"/> containing the result.</returns>
    Task<IMacroNameResourceDataDictionary> ReadLandmarkNames(IXmlReaderWrapper reader);

    /// <summary>
    /// Reads offset data from the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> containing the resource data to parse.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  A <see cref="IOffsetDataDictionary"/> containing the result.</returns>
    Task<IOffsetDataDictionary> ReadOffsets(IXmlReaderWrapper reader);

    /// <summary>
    /// Parses the sector names from the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> containing the resource data to parse.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IMacroNameResourceDataDictionary"/> containing the result.</returns>
    Task<IMacroNameResourceDataDictionary> ReadSectorNames(IXmlReaderWrapper reader);

    /// <summary>
    /// Parses the ship models from the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> containing the resource data to parse.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IShipModelResourceDataDictionary"/> containing the result.</returns>
    Task<IShipModelResourceDataDictionary> ReadShipModels(IXmlReaderWrapper reader);

    /// <summary>
    /// Reads text resource data from the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> containing the resource data to parse.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  A <see cref="ITextResourcePageDictionary"/> containing the result.</returns>
    Task<ITextResourcePageDictionary> ReadTextResources(IXmlReaderWrapper reader);

    /// <summary>
    /// Parses the wares from the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> containing the resource data to parse.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IWareDataDictionary"/> containing the result.</returns>
    Task<IWareDataDictionary> ReadWares(IXmlReaderWrapper reader);
}
