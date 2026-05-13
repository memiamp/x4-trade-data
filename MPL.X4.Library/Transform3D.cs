namespace MPL.X4;

/// <summary>
/// A record that implements a 3D transform.
/// </summary>
/// <param name="Position">An <see cref="IPosition3D"/> that is the position.</param>
/// <param name="Quaternion">An <see cref="IQuaternion"/> that is the quaternion.</param>
/// <param name="Rotation">An <see cref="IRotation3D"/> that is the rotation.</param>
public readonly record struct Transform3D(
                                          IPosition3D Position,
                                          IQuaternion Quaternion,
                                          IRotation3D Rotation)
    : ITransform3D
{
    /// <summary>
    /// Creates a new instance of the <see cref="Transform3D"/> record by copying the values from the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="ITransform3D"/> to copy values from.</param>
    public Transform3D(ITransform3D source)
        : this(source.Position, source.Quaternion, source.Rotation)
    {
    }

    public override string ToString()
        => $"{Position} {Rotation} {Quaternion}";
}
