namespace MPL.X4;

/// <summary>
/// A class that implements a sector rotation.
/// </summary>
public class SectorRotation() : ISectorRotation
{
    /// <summary>
    /// Creates a new instance of the <see cref="SectorRotation"/> class by copying the values from the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="ISectorRotation"/> to copy values from.</param>
    public SectorRotation(ISectorRotation source)
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
