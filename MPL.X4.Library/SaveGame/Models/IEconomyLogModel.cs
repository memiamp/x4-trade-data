namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of an economy log.
/// </summary>
public interface IEconomyLogModel
{
    /// <summary>
    /// Gets the trade log.
    /// </summary>
    ITradeLogEntryModelList TradeLog { get; }
}
