namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a station's build storage trade partner.
/// </summary>
/// <param name="tradePartner">An <see cref="IStationModel"/> that is the trade partner.</param>
internal class TradePartnerStationBuildStorageModel(
                                                    IStationModel tradePartner)
    : TradePartnerStationBaseModel(tradePartner, TradePartnerType.BuildStorage)
{
}
