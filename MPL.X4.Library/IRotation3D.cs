namespace MPL.X4;

/// <summary>
/// An interface that defines a 3D rotation.
/// </summary>
public interface IRotation3D
{
    /// <summary>
    /// Gets the default rotation.
    /// </summary>
    public static IRotation3D GetDefault()
        => new Rotation3D 
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
