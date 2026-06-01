namespace MPL.X4;

/// <summary>
/// An interface that defines a 3D position.
/// </summary>
public interface IPosition3D
{
    /// <summary>
    /// Adds the specified <paramref name="value"/> and returns the result.
    /// </summary>
    /// <param name="value">An <see cref="IPosition3D"/> to add.</param>
    /// <returns>An <see cref="IPosition3D"/> that is the result.</returns>
    IPosition3D Add(IPosition3D value)
        => new Position3D(X + value.X, Y + value.Y, Z + value.Z);

    /// <summary>
    /// Gets the default position.
    /// </summary>
    public static IPosition3D GetDefault()
        => new Position3D(0, 0, 0);

    /// <summary>
    /// Gets an indication of whether this is set to default values.
    /// </summary>
    bool IsDefault => X == 0 && Y == 0 && Z == 0;

    /// <summary>
    /// Gets the X position.
    /// </summary>
    double X { get; }

    /// <summary>
    /// Gets the Y position.
    /// </summary>
    double Y { get; }

    /// <summary>
    /// Gets the Z position.
    /// </summary>
    double Z { get; }
}
