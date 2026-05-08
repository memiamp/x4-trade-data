namespace MPL.X4;

/// <summary>
/// An interface that defines a 3D transform.
/// </summary>
public interface ITransform3D
{
    /// <summary>
    /// Gets the default transform.
    /// </summary>
    public static ITransform3D GetDefault()
        => new Transform3D 
        {
            Position = IPosition3D.GetDefault(),
            Quaternion = IQuaternion.GetDefault(),
            Rotation = IRotation3D.GetDefault()
        };

    /// <summary>
    /// Gets the position.
    /// </summary>
    IPosition3D Position { get; }

    /// <summary>
    /// Gets the quartenion.
    /// </summary>
    IQuaternion Quaternion { get; }

    /// <summary>
    /// Gets the rotation.
    /// </summary>
    IRotation3D Rotation { get; }
}
