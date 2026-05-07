namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a zone offset.
/// </summary>
internal class ZoneOffset : Offset, IZoneOffset
{
    public override string ToString()
        => $"{MacroName} - {Position} {Rotation}";

    public required string MacroName { get; init; }
}
