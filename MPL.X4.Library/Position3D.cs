namespace MPL.X4;

/// <summary>
/// A class that implements a sector position.
/// </summary>
public class SectorPosition() : ISectorPosition
{
    /// <summary>
    /// Creates a new instance of the <see cref="SectorPosition"/> class by copying the values from the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="ISectorPosition"/> to copy values from.</param>
    public SectorPosition(ISectorPosition source)
        : this()
    {
        X = source.X;
        Y = source.Y;
        Z = source.Z;
    }

    public override string ToString()
        => $"{X},{Y},{Z}";

    public double X { get; set; }

    public double Y { get; set; }

    public double Z { get; set; }
}
