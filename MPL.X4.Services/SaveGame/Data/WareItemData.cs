namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of an item of ware.
/// </summary>
internal class WareItemData : IWareItemData
{
    public override string ToString()
        => $"{Ware} - {Amount} - {Buy} {Sell} {Price}";

    public required int Amount { get; init; }

    public required int Buy { get; init; }

    public required int Price { get; init; }

    public required int Sell { get; init; }

    public required string Ware { get; init; }
}
