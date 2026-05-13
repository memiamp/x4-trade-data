namespace MPL.X4;

/// <summary>
/// A record that implements a quaternion.
/// </summary>
/// <param name="W">A <see cref="double"/> indicating the W value.</param>
/// <param name="X">A <see cref="double"/> indicating the X value.</param>
/// <param name="Y">A <see cref="double"/> indicating the Y value.</param>
/// <param name="Z">A <see cref="double"/> indicating the Z value.</param>
public readonly record struct QuaternionRecord(
                                               double W,
                                               double X,
                                               double Y,
                                               double Z)
    : IQuaternion
{
    /// <summary>
    /// Creates a new instance of the <see cref="QuaternionRecord"/> record by copying the values from the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="IQuaternion"/> to copy values from.</param>
    public QuaternionRecord(IQuaternion source)
        : this(source.W, source.X, source.Y, source.Z)
    {
    }

    public override string ToString()
        => $"{X},{Y},{Z},{W}";
}
