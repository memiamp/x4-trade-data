using MPL.X4.GameResources.Models;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a trade partner.
/// </summary>
public interface ITradePartnerModel : IHasCode, IHasOwner, IModelWithId
{
    /// <summary>
    /// Gets an indication of whether this trade partner is removed (i.e. deleted).
    /// </summary>
    bool IsRemoved { get; }

    /// <summary>
    /// Gets the name of the trade partner.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the type of the trade partner.
    /// </summary>
    TradePartnerType Type { get; }
}
