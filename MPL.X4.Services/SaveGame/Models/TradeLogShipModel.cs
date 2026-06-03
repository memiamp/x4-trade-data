namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a trade log ship.
/// </summary>
internal class TradeLogShipModel : ModelWithIdBase, ITradeLogShipModel
{
    public override string ToString()
        => $"{Name} {Code} - Trades: {Trades.Count} - {Id}";

    public required string Code { get; init; }

    public required string Name { get; init; }

    public required ITradeLogTradeModelList Trades { get; init; }
}
