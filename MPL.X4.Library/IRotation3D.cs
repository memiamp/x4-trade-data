namespace MPL.X4;

/// <summary>
/// An interface that defines a sector rotation.
/// </summary>
public interface ISectorRotation
{
    /// <summary>
    /// Gets the default sector rotation.
    /// </summary>
    public static ISectorRotation GetDefault()
        => new SectorRotation
        {
            Pitch = 0,
            Roll = 0,
            Yaw = 0,
        };

    /// <summary>
    /// Gets the pitch.
    /// </summary>
    double Pitch { get; }

    /// <summary>
    /// Gets the roll.
    /// </summary>
    double Roll { get; }

    /// <summary>
    /// Gets the yaw.
    /// </summary>
    double Yaw { get; }
}
