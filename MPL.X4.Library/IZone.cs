namespace MPL.X4;

/// <summary>
/// An interface that defines a zone.
/// </summary>
public interface IZone
{
    /// <summary>
    /// Gets the code of the zone.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the identifier of the zone.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets an indication of whether the station is known to the player.
    /// </summary>
    bool IsKnown { get; }

    /// <summary>
    /// Gets the lockboxes in the zone.
    /// </summary>
    IEnumerable<ILockbox> Lockboxes { get; }

    /// <summary>
    /// Gets the position of the zone.
    /// </summary>
    ISectorPosition Position { get; }

    /// <summary>
    /// Gets the ships in the zone.
    /// </summary>
    IEnumerable<IShip> Ships { get; }

    /// <summary>
    /// Gets the stations in the zone.
    /// </summary>
    IEnumerable<IStation> Stations { get; }
}
