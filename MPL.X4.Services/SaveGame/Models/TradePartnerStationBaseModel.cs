using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a base model of a station trade partner.
/// </summary>
/// <param name="tradePartner">An <see cref="IStationModel"/> that is the trade partner.</param>
/// <param name="type">A <see cref="TradePartnerType"/> that is the partner type.</param>
internal class TradePartnerStationBaseModel(
                                            IStationModel tradePartner,
                                            TradePartnerType type)
    : TradePartnerModelBase<IStationModel>(tradePartner, type)
{
    public override string Name => TradePartner.Name;
}
