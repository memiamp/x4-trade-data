namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a ship trade partner.
/// </summary>
/// <param name="tradePartner">An <see cref="IShipModel"/> that is the trade partner.</param>
internal class TradePartnerShipModel(
                                     IShipModel tradePartner)
    : TradePartnerModelBase<IShipModel>(tradePartner, TradePartnerType.Ship)
{
    public override string Name => TradePartner.Name ?? TradePartner.Model;
}
