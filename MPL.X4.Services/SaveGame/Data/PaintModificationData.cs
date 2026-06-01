namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a paint modification.
/// </summary>
internal class PaintModificationData : ModificationData, IPaintModificationData
{
    public override string ToString()
        => $"{Ware} - Generated: {Generated}";

    public required bool Generated { get; init; }
}
