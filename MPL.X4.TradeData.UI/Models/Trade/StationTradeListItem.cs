using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.Trade;

/// <summary>
/// A class that implements a list item for a trade from a station.
/// </summary>
/// <param name="sector">An <see cref="ISectorModel"/> that is the sector model.</param>
/// <param name="station">An <see cref="IStationModel"/> that is the station model.</param>
/// <param name="trade">An <see cref="ITradeModel"/> that is the trade model.</param>
internal class StationTradeListItem(
                                    ISectorModel sector,
                                    IStationModel station,
                                    ITradeModel trade)
    : ITradeListItem
{
    IFactionModel ITradeListItem.SectorFaction => sector.Owner;

    string ITradeListItem.SectorName => sector.Name;

    IFactionModel ITradeListItem.StationFaction => station.Owner;

    string ITradeListItem.StationName => station.Name;

    ITradeModel ITradeListItem.Trade => trade;
}
