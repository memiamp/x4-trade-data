namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of an item of cargo.
/// </summary>
internal class CargoItemData : ICargoItemData
{
    public override string ToString()
        => $"{Ware} ({Amount})";

    public required int Amount { get; init; }

    public required string Ware { get; init; }
}
