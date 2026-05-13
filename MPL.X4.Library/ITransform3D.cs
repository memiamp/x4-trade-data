namespace MPL.X4;

/// <summary>
/// An interface that defines a 3D transform.
/// </summary>
public interface ITransform3D
{
    /// <summary>
    /// Adds the specified <paramref name="value"/> and returns the result.
    /// </summary>
    /// <param name="value">An <see cref="ITransform3D"/> to add.</param>
    /// <returns>An <see cref="ITransform3D"/> that is the result.</returns>
    ITransform3D Add(ITransform3D value)
        => new Transform3D(
                           Position.Add(value.Position),
                           Quaternion.Add(value.Quaternion),
                           Rotation.Add(value.Rotation));

    /// <summary>
    /// Gets the default transform.
    /// </summary>
    public static ITransform3D GetDefault()
        => new Transform3D(
                           IPosition3D.GetDefault(),
                           IQuaternion.GetDefault(),
                           IRotation3D.GetDefault());

    /// <summary>
    /// Gets an indication of whether this is set to default values.
    /// </summary>
    bool IsDefault => Position.IsDefault && Quaternion.IsDefault && Rotation.IsDefault;

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
