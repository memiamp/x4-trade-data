namespace MPL.X4;

/// <summary>
/// A class that implements a 3D rotation.
/// </summary>
public class Rotation3D() : IRotation3D
{
    /// <summary>
    /// Creates a new instance of the <see cref="Rotation3D"/> class by copying the values from the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="IRotation3D"/> to copy values from.</param>
    public Rotation3D(IRotation3D source)
        : this()
    {
        Pitch = source.Pitch;
        Roll = source.Roll;
        Yaw = source.Yaw;
    }

    public override string ToString()
        => $"{Pitch},{Roll},{Yaw}";

    public double Pitch { get; set; }

    public double Roll { get; set; }

    public double Yaw { get; set; }
}
