namespace MPL.X4;

/// <summary>
/// A record that implements a 3D rotation.
/// </summary>
/// <param name="Pitch">A <see cref="double"/> indicating the pitch.</param>
/// <param name="Roll">A <see cref="double"/> indicating the roll.</param>
/// <param name="Yaw">A <see cref="double"/> indicating the yaw.</param>
public readonly record struct Rotation3D(
                                         double Pitch,
                                         double Roll,
                                         double Yaw)
    : IRotation3D
{
    /// <summary>
    /// Creates a new instance of the <see cref="Rotation3D"/> record by copying the values from the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="IRotation3D"/> to copy values from.</param>
    public Rotation3D(IRotation3D source)
        : this(source.Pitch, source.Roll, source.Yaw)
    {
    }

    public override string ToString()
        => $"{Pitch},{Roll},{Yaw}";
}
