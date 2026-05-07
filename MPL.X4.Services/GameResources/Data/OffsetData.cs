namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements an offset.
/// </summary>
internal class Offset : IOffset
{
    public override string ToString()
        => $"{Position} {Rotation}";

    public required ISectorPosition Position { get; init; }

    public required ISectorRotation Rotation { get; init; }
}
