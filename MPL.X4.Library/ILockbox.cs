namespace MPL.X4;

/// <summary>
/// An interface that defines a lockbox.
/// </summary>
public interface ILockbox
{
    /// <summary>
    /// Gets the code of the lockbox.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the identifier of the lockbox.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets an indication of whether the lockbox is known to the player.
    /// </summary>
    bool IsKnown { get; }

    /// <summary>
    /// Gets the position of the station.
    /// </summary>
    ISectorPosition Position { get; }

    /// <summary>
    /// Gets the lockbox type.
    /// </summary>
    string Type { get; }
}
