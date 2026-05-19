namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a trade.
/// </summary>
public interface ITradeModel : IModelWithId
{
    /// <summary>
    /// Gets the amount of the ware to trade.
    /// </summary>
    int Amount { get; }

    /// <summary>
    /// Gets the name of the traded ware.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the price of the trade.
    /// </summary>
    int Price { get; }

    /// <summary>
    /// Gets the type of the trade.
    /// </summary>
    TradeType Type { get; }
}
