namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a trade.
/// </summary>
internal class TradeData : ITradeData
{
    public override string ToString()
        => $"{Ware} {Price} - Buy {AmountToBuy} Sell {AmountToSell} - {Id}";

    public required int AmountToBuy { get; init; }

    public required int AmountToSell { get; init; }

    public required string Id { get; init; }

    public required int Price { get; init; }

    public required string Ware { get; init; }
}
