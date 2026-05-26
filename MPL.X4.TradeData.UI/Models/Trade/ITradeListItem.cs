using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.Trade;

/// <summary>
/// An interface that defines a list item for a trade.
/// </summary>
internal interface ITradeListItem
{
    /// <summary>
    /// Gets the sector faction.
    /// </summary>
    internal IFactionModel SectorFaction { get; }

    /// <summary>
    /// Gets the sector name.
    /// </summary>
    internal string SectorName { get; }

    /// <summary>
    /// Gets the station faction.
    /// </summary>
    internal IFactionModel StationFaction { get; }

    /// <summary>
    /// Gets the station name.
    /// </summary>
    internal string StationName { get; }

    /// <summary>
    /// Gets the trade.
    /// </summary>
    internal ITradeModel Trade { get; }
}
