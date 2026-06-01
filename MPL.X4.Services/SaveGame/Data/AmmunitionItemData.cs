namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of an item of ammunition.
/// </summary>
internal class AmmunitionItemData : IAmmunitionItemData
{
    public override string ToString()
        => $"{Macro} - {Amount} - {Exact}";

    public required int Amount { get; init; }

    public required int Exact { get; init; }

    public required string Macro { get; init; }
}
