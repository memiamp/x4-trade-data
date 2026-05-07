using MPL.X4.GameResources.Data;

namespace MPL.X4.Models;

/// <summary>
/// A class that implements an offset data model.
/// </summary>
internal class OffsetData : IOffsetData
{
    public override string ToString()
        => $"{Position} {Rotation}";

    public required IPosition3D Position { get; init; }

    public required IRotation3D Rotation { get; init; }
}
