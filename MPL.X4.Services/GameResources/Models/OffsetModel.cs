namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements an offset model.
/// </summary>
internal class OffsetModel : ModelWithIdBase, IOffsetModel
{
    public override string ToString()
        => $"{Name} - {Offset}";

    public required string Name { get; init; }

    public required ITransform3D Offset { get; init; }
}
