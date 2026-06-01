namespace MPL.X4;

/// <summary>
/// An interface that defines a quaternion.
/// </summary>
public interface IQuaternion
{
    /// <summary>
    /// Adds the specified <paramref name="value"/> and returns the result.
    /// </summary>
    /// <param name="value">An <see cref="IQuaternion"/> to add.</param>
    /// <returns>An <see cref="IQuaternion"/> that is the result.</returns>
    IQuaternion Add(IQuaternion value)
        => new QuaternionRecord(W + value.W, X + value.X, Y + value.Y, Z + value.Z);

    /// <summary>
    /// Gets the default quartenion.
    /// </summary>
    public static IQuaternion GetDefault()
        => new QuaternionRecord(0, 0, 0, 0);

    /// <summary>
    /// Gets an indication of whether this is set to default values.
    /// </summary>
    bool IsDefault => W == 0 && X == 0 && Y == 0 && Z == 0;

    /// <summary>
    /// Gets the W.
    /// </summary>
    double W { get; }

    /// <summary>
    /// Gets the X.
    /// </summary>
    double X { get; }

    /// <summary>
    /// Gets the Y.
    /// </summary>
    double Y { get; }

    /// <summary>
    /// Gets the Z.
    /// </summary>
    double Z { get; }
}
