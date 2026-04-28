namespace MPL.X4;

/// <summary>
/// An interface that defines a sector.
/// </summary>
public interface ISector
{
    /// <summary>
    /// Gets the code of the sector.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the gates in the sector.
    /// </summary>
    IEnumerable<IGate> Gates { get; }

    /// <summary>
    /// Gets the identifier of the sector.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets an indication of whether the sector is known to the player.
    /// </summary>
    bool IsKnown { get; }

    /// <summary>
    /// Gets the lockboxes in the sector.
    /// </summary>
    IEnumerable<ILockbox> Lockboxes { get; }

    /// <summary>
    /// Gets the name identifier of the sector.
    /// </summary>
    int NameId { get; }

    /// <summary>
    /// Gets the owner of the sector.
    /// </summary>
    string Owner { get; }

    /// <summary>
    /// Gets the ships in the sector.
    /// </summary>
    IEnumerable<IShip> Ships { get; }

    /// <summary>
    /// Gets the stations in the sector.
    /// </summary>
    IEnumerable<IStation> Stations { get; }
}
