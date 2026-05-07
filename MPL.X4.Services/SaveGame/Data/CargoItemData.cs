namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements an item of cargo.
/// </summary>
internal class CargoItem : ICargoItem
{
    public override string ToString()
        => $"{Ware} ({Amount})";

    public required int Amount { get; init; }

    public required string Ware { get; init; }
}
