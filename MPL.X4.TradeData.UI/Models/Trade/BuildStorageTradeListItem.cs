using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.Trade;

/// <summary>
/// A class that implements a list item for a trade from a build storage.
/// </summary>
/// <param name="buildStorage">An <see cref="IBuildStorageModel"/> that is the build storage model.</param>
/// <param name="sector">An <see cref="ISectorModel"/> that is the sector model.</param>
/// <param name="trade">An <see cref="ITradeModel"/> that is the trade model.</param>
internal class BuildStorageTradeListItem(
                                         IBuildStorageModel buildStorage,
                                         ISectorModel sector,
                                         ITradeModel trade)
    : ITradeListItem
{
    IFactionModel ITradeListItem.SectorFaction => sector.Owner;

    string ITradeListItem.SectorName => sector.Name;

    IFactionModel ITradeListItem.StationFaction => buildStorage.Owner;

    string ITradeListItem.StationName => buildStorage.Code;

    ITradeModel ITradeListItem.Trade => trade;
}
