namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a trade log trade.
/// </summary>
internal class TradeLogTradeModel : TradeModel, ITradeLogTradeModel
{
    public override string ToString()
        => $"{Name} {Amount} {Type} {Price} - {Id}";

    public required IStationModel? BoughtFrom { get; init; }

    public required IStationModel? SoldTo { get; init; }
}
