namespace MPL.X4;

/// <summary>
/// An interface that defines a trade.
/// </summary>
public interface ITrade
{
    /// <summary>
    /// Gets the amount of the trade ware available to buy.
    /// </summary>
    int AmountToBuy { get; }

    /// <summary>
    /// Gets the amount of the trade ware available to sell.
    /// </summary>
    int AmountToSell { get; }

    /// <summary>
    /// Gets the identifier of the trade.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the price of the trade ware.
    /// </summary>
    int Price { get; }

    /// <summary>
    /// Gets the name of the trade ware.
    /// </summary>
    string Ware { get; }
}
