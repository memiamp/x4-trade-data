namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a trade.
/// </summary>
internal class Trade : ITrade
{
    public override string ToString()
        => $"{Ware} {Price} - Buy {AmountToBuy} Sell {AmountToSell} - {Id}";

    public required int AmountToBuy { get; init; }

    public required int AmountToSell { get; init; }

    public required string Id { get; init; }

    public required int Price { get; init; }

    public required string Ware { get; init; }
}
