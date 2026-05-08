namespace MPL.X4;

/// <summary>
/// An interface that defines a quaternion.
/// </summary>
public interface IQuaternion
{
    /// <summary>
    /// Gets the default quartenion.
    /// </summary>
    public static IQuaternion GetDefault()
        => new QuaternionRecord
        {
            W = 0,
            X = 0,
            Y = 0,
            Z = 0
        };

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
