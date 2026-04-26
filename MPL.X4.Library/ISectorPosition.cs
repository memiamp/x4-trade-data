namespace MPL.X4;

/// <summary>
/// An interface that defines a sector position.
/// </summary>
public interface ISectorPosition
{
    /// <summary>
    /// Gets the default sector position.
    /// </summary>
    public static ISectorPosition GetDefault()
        => new SectorPosition
        {
            X = 0,
            Y = 0,
            Z = 0,
        };

    /// <summary>
    /// Gets the X position.
    /// </summary>
    int X { get; }

    /// <summary>
    /// Gets the Y position.
    /// </summary>
    int Y { get; }

    /// <summary>
    /// Gets the Z position.
    /// </summary>
    int Z { get; }
}
