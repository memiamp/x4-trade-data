namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a trade.
/// </summary>
internal class TradeModel : ModelWithIdBase, ITradeModel
{
    public override string ToString()
        => $"{Name} {Amount} {Type} {Price} - {Id}";

    public required int Amount { get; init; }

    public required string Name { get; init; }

    public required int Price { get; init; }

    public required TradeType Type { get; init; }
}
