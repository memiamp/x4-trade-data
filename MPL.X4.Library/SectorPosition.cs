namespace MPL.X4;

/// <summary>
/// A class that implements a sector position.
/// </summary>
public class SectorPosition : ISectorPosition
{
    public override string ToString()
        => $"{X},{Y},{Z}";

    public required int X { get; init; }

    public required int Y { get; init; }

    public required int Z { get; init; }
}
