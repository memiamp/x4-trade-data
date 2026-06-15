namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a station trade partner.
/// </summary>
/// <param name="tradePartner">An <see cref="IStationModel"/> that is the trade partner.</param>
internal class TradePartnerStationModel(
                                        IStationModel tradePartner)
    : TradePartnerStationBaseModel(tradePartner, TradePartnerType.Station)
{
}
