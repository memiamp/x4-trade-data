namespace MPL.X4;

/// <summary>
/// A record that implements a 3D position.
/// </summary>
/// <param name="X">A <see cref="double"/> indicating the X position.</param>
/// <param name="Y">A <see cref="double"/> indicating the Y position.</param>
/// <param name="Z">A <see cref="double"/> indicating the Z position.</param>
public readonly record struct Position3D(
                                         double X,
                                         double Y,
                                         double Z)
    : IPosition3D
{
    /// <summary>
    /// Creates a new instance of the <see cref="Position3D"/> record by copying the values from the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="IPosition3D"/> to copy values from.</param>
    public Position3D(IPosition3D source)
        : this(source.X, source.Y, source.Z)
    {
    }

    public override string ToString()
        => $"{X},{Y},{Z}";
}
