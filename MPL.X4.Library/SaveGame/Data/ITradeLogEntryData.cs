namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a trade log entry.
/// </summary>
public interface ITradeLogEntryData : ILogEntryData
{
    /// <summary>
    /// Gets the identifeir of the buyer.
    /// </summary>
    string BuyerId { get; }

    /// <summary>
    /// Gets the price of the trade.
    /// </summary>
    /// <remarks>This is the price per traded unit - not the total of the trade.</remarks>
    int Price { get; }

    /// <summary>
    /// Gets the identifeir of the seller.
    /// </summary>
    string SellerId { get; }

    /// <summary>
    /// Gets the trade volume.
    /// </summary>
    int Volume { get; }

    /// <summary>
    /// Gets the ware that was traded.
    /// </summary>
    string Ware { get; }
}
