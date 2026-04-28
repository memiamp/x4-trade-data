namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a zone offset.
/// </summary>
internal class ZoneOffset : IZoneOffset
{
    public override string ToString()
        => $"{MacroName} - {OffsetPosition} {OffsetRotation}";

    public required string MacroName { get; init; }

    public required ISectorPosition OffsetPosition { get; init; }

    public required ISectorRotation OffsetRotation { get; init; }
}
