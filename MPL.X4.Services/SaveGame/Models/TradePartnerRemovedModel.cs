using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a removed trade partner.
/// </summary>
/// <param name="owner">An <see cref="IFactionModel"/> that is the owner of the trade partner.</param>
/// <param name="tradePartner">An <see cref="IShipModel"/> that is the trade partner.</param>
internal class TradePartnerRemovedModel(
                                        IFactionModel owner,
                                        IRemovedObjectData tradePartner)
    : ITradePartnerModel
{
    public override string ToString()
        => $"{Name} ({Code}) - {Type}";

    public string Code => tradePartner.Code;

    public string Id => tradePartner.Id;

    public bool IsRemoved => true;

    public string Name => tradePartner.Name;

    public IFactionModel Owner => owner;

    public TradePartnerType Type => TradePartnerType.Removed;
}
