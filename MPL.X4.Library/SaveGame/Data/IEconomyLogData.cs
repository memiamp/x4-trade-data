namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of an economy log.
/// </summary>
public interface IEconomyLogData
{
    /// <summary>
    /// Gets removed objects.
    /// </summary>
    IEnumerable<IRemovedObjectData> RemovedObjects { get; }

    /// <summary>
    /// Gets the trade log.
    /// </summary>
    ITradeLogData TradeLog { get; }
}
