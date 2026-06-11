namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a trade log entry.
/// </summary>
public interface ITradeLogEntryModel : ITradeModel
{
    /// <summary>
    /// Gets the buying partner of the trade.
    /// </summary>
    ITradePartnerModel Buyer { get; }

    /// <summary>
    /// Gets the selling partner of the trade.
    /// </summary>
    ITradePartnerModel Seller { get; }

    /// <summary>
    /// Gets the actual time of the trade.
    /// </summary>
    double Time { get; }

    /// <summary>
    /// Gets the length of time since the trade was made until now.
    /// </summary>
    TimeSpan TradeAge { get; }
}
