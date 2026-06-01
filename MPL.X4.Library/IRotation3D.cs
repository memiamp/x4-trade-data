namespace MPL.X4;

/// <summary>
/// An interface that defines a 3D rotation.
/// </summary>
public interface IRotation3D
{
    /// <summary>
    /// Adds the specified <paramref name="value"/> and returns the result.
    /// </summary>
    /// <param name="value">An <see cref="IRotation3D"/> to add.</param>
    /// <returns>An <see cref="IRotation3D"/> that is the result.</returns>
    IRotation3D Add(IRotation3D value)
        => new Rotation3D(Pitch + value.Pitch, Roll + value.Roll, Yaw + value.Yaw);

    /// <summary>
    /// Gets the default rotation.
    /// </summary>
    public static IRotation3D GetDefault()
        => new Rotation3D(0, 0, 0);

    /// <summary>
    /// Gets an indication of whether this is set to default values.
    /// </summary>
    bool IsDefault => Pitch == 0 && Roll == 0 && Yaw == 0;

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
