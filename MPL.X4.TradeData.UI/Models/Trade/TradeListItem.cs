using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.Trade;

/// <summary>
/// A class that implements a list item for a trade.
/// </summary>
/// <param name="sector">An <see cref="ISectorModel"/> that is the sector model.</param>
/// <param name="station">An <see cref="IStationModel"/> that is the station model.</param>
/// <param name="trade">An <see cref="ITradeModel"/> that is the trade model.</param>
internal class TradeListItem(
                             ISectorModel sector,
                             IStationModel station,
                             ITradeModel trade)
{
    /// <summary>
    /// Gets the sector faction.
    /// </summary>
    internal IFactionModel SectorFaction => sector.Owner;

    /// <summary>
    /// Gets the sector name.
    /// </summary>
    internal string SectorName => sector.Name;

    /// <summary>
    /// Gets the station faction.
    /// </summary>
    internal IFactionModel StationFaction => station.Owner;

    /// <summary>
    /// Gets the station name.
    /// </summary>
    internal string StationName => station.Name;

    /// <summary>
    /// Gets the trade.
    /// </summary>
    internal ITradeModel Trade => trade;
}
