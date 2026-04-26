namespace MPL.X4;

/// <summary>
/// A class that implements a sector position.
/// </summary>
public class SectorPosition : ISectorPosition
{
    public override string ToString()
        => $"{X},{Y},{Z}";

    public required int X { get; set; }

    public required int Y { get; set; }

    public required int Z { get; set; }
}
