namespace MPL.X4;

/// <summary>
/// A class that implements a 3D position.
/// </summary>
public class Position3D() : IPosition3D
{
    /// <summary>
    /// Creates a new instance of the <see cref="Position3D"/> class by copying the values from the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="IPosition3D"/> to copy values from.</param>
    public Position3D(IPosition3D source)
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
