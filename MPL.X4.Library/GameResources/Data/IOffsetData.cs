namespace MPL.X4;

/// <summary>
/// An interface that defines an offset.
/// </summary>
public interface IOffset
{
    /// <summary>
    /// Gets the position.
    /// </summary>
    ISectorPosition Position { get; }

    /// <summary>
    /// Gets the rotation.
    /// </summary>
    ISectorRotation Rotation { get; }
}
