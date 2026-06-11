namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of an economy log.
/// </summary>
internal class EconomyLogData : IEconomyLogData
{
    public override string ToString()
        => $"Trade Log: {TradeLog.Count()}";

    public required IEnumerable<IRemovedObjectData> RemovedObjects { get; init; }

    public required ITradeLogData TradeLog { get; init; }
}
