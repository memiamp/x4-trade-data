using MPL.X4.TradeData.Services;
using MPL.X4.TradeData.UI.Models;

namespace MPL.X4.TradeData.UI.Services;

/// <summary>
/// An interface that defines the behaviour of a model mapper.
/// </summary>
internal interface IModelMapper
{
    /// <summary>
    /// Maps a ship class from the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">A <see cref="string"/> containing the source to map.</param>
    /// <returns>A <see cref="ShipClass"/> that is the result.</returns>
    ShipClass MapShipClass(string source);

    /// <summary>
    /// Maps a special item model from the specified parameters.
    /// </summary>
    /// <param name="sectorNameId">An <see cref="int"/> that is the identifier of the sector name to map.</param>
    /// <param name="source">An <see cref="ILockbox"/> that is the source to map from.</param>
    /// <param name="resourceData">An <see cref="IResourceData"/> that is the resource data to use.</param>
    /// <returns>A <see cref="SpecialItem"/> that is the result.</returns>
    SpecialItem MapSpecialItem(int sectorNameId, ILockbox source, IResourceData resourceData);

    /// <summary>
    /// Maps a special item model from the specified parameters.
    /// </summary>
    /// <param name="sectorNameId">An <see cref="int"/> that is the identifier of the sector name to map.</param>
    /// <param name="source">An <see cref="IShip"/> that is the source to map from.</param>
    /// <param name="resourceData">An <see cref="IResourceData"/> that is the resource data to use.</param>
    /// <returns>A <see cref="SpecialItem"/> that is the result.</returns>
    SpecialItem MapSpecialItem(int sectorNameId, IShip source, IResourceData resourceData);

    /// <summary>
    /// Maps special items from the specified parameters.
    /// </summary>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of <see cref="ISector"/> that is the source to map from.</param>
    /// <param name="resourceData">An <see cref="IResourceData"/> that is the resource data to use.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="SpecialItem"/> that is the result.</returns>
    IEnumerable<SpecialItem> MapSpecialItems(IEnumerable<ISector> source, IResourceData resourceData);

    /// <summary>
    /// Maps a trade offer model from the specified parameters.
    /// </summary>
    /// <param name="sector">An <see cref="ISector"/> that is the sector to map from.</param>
    /// <param name="station">An <see cref="IStation"/> that is the station to map from.</param>
    /// <param name="trade">An <see cref="ITrade"/> that is the trade to map from.</param>
    /// <param name="resourceData">An <see cref="IResourceData"/> that is the resource data to use.</param>
    /// <returns>A <see cref="TradeOffer"/> that is the result.</returns>
    TradeOffer MapTradeOffer(ISector sector, IStation station, ITrade trade, IResourceData resourceData);

    /// <summary>
    /// Maps trade offers from the specified parameters.
    /// </summary>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of <see cref="ISector"/> that is the source to map from.</param>
    /// <param name="resourceData">An <see cref="IResourceData"/> that is the resource data to use.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="TradeOffer"/> that is the result.</returns>
    IEnumerable<TradeOffer> MapTradeOffers(IEnumerable<ISector> source, IResourceData resourceData);
}
