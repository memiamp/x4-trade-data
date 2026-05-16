namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a sector name model.
/// </summary>
internal class SectorNameModel : ModelWithIdBase, ISectorNameModel
{
    public override string ToString()
        => $"{Name} - {Id}";

    public required string Name { get; init; }
}
