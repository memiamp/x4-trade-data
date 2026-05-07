namespace MPL.X4;

/// <summary>
/// An interface that defines a 3D position.
/// </summary>
public interface IPosition3D
{
    /// <summary>
    /// Gets the default position.
    /// </summary>
    public static IPosition3D GetDefault()
        => new Position3D
        {
            X = 0,
            Y = 0,
            Z = 0,
        };

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
