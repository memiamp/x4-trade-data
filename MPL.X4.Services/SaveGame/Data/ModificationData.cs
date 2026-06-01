namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a modification.
/// </summary>
internal class ModificationData : IModificationData
{
    public override string ToString()
        => $"{Ware}";

    public required string Ware { get; init; }
}
