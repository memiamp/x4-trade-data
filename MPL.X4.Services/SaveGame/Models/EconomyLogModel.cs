namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of an economy log.
/// </summary>
internal class EconomyLogModel : IEconomyLogModel
{
    public override string ToString()
        => $"Trades: {TradeLog.Count}";

    public required ITradeLogShipModelList TradeLog { get; init; }
}
