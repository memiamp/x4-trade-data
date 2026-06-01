namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements an offset data model.
/// </summary>
internal class OffsetData : IOffsetData
{
    public override string ToString()
        => $"{Name} ({Macro}) - {ReferenceType} - {Offset}";

    public required string Macro { get; init; }

    public required string Name { get; init; }

    public required ITransform3D Offset { get; init; }

    public required string ReferenceType { get; init; }
}
