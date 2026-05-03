namespace MPL.X4;

/// <summary>
/// An interface that defines a ship.
/// </summary>
public interface IShip
{
    /// <summary>
    /// Gets the ship cargo.
    /// </summary>
    ICargo Cargo { get; }

    /// <summary>
    /// Gets the class of the ship.
    /// </summary>
    string Class { get; }

    /// <summary>
    /// Gets the code of the ship.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the identifier of the ship.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets an indication of whether the ship is known to the player.
    /// </summary>
    bool IsKnown { get; }

    /// <summary>
    /// Gets the macro of the ship.
    /// </summary>
    string Macro { get; }

    /// <summary>
    /// Gets the owner of the ship.
    /// </summary>
    string Owner { get; }

    /// <summary>
    /// Gets the position of the ship.
    /// </summary>
    ISectorPosition Position { get; }
}
