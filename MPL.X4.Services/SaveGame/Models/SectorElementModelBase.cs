namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines the base model of a sector element.
/// </summary>
internal class SectorElementModelBase : SectorModelBase, IIsWreckable
{
    public required bool IsWreck { get; init; }
}
