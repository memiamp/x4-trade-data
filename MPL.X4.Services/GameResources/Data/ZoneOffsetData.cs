using MPL.X4.GameResources.Data;

namespace MPL.X4.Models;

/// <summary>
/// A class that implements a zone offset data model.
/// </summary>
internal class ZoneOffsetData : OffsetData, IZoneOffsetData
{
    public override string ToString()
        => $"{MacroName} - {Position} {Rotation}";

    public required string MacroName { get; init; }
}
