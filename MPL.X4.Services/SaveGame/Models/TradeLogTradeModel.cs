namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a trade log trade.
/// </summary>
internal class TradeLogTradeModel : TradeModel, ITradeLogTradeModel
{
    public override string ToString()
        => $"{Name} {Amount} {Type} {Price} - {Id}";

    public required string TargetCode { get; init; }

    public required string TargetName { get; init; }

    public required TradeTargetType TargetType { get; init; }
}
