using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a base model of a trade partner.
/// </summary>
/// <typeparam name="TModel">The type of the trade partner, which must implement <see cref="IHasCode"/> and <see cref="IModelWithId"/>.</typeparam>
/// <param name="tradePartner">A <typeparamref name="TModel"/> that is the trade partner.</param>
/// <param name="type">A <see cref="TradePartnerType"/> that is the partner type.</param>
internal abstract class TradePartnerModelBase<TModel>(
                                                      TModel tradePartner,
                                                      TradePartnerType type)
    : ITradePartnerModel
    where TModel : IHasCode, IHasOwner, IModelWithId
{
    public override string ToString()
        => $"{Name} ({Code}) - {Type}";

    /// <summary>
    /// Gets the trade partner.
    /// </summary>
    private protected TModel TradePartner => tradePartner;

    public string Code => tradePartner.Code;

    public string Id => tradePartner.Id;

    public bool IsRemoved => false;

    public abstract string Name { get; }

    public IFactionModel Owner => tradePartner.Owner;

    public TradePartnerType Type => type;
}