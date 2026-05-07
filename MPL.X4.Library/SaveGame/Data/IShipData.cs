namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a ship.
/// </summary>
public interface IShipData
{
    /// <summary>
    /// Gets the ship cargo.
    /// </summary>
    ICargoData Cargo { get; }

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
    /// Gets the modifications applied to the ship.
    /// </summary>
    IEnumerable<string> Modifications { get; }

    /// <summary>
    /// Gets the owner of the ship.
    /// </summary>
    string Owner { get; }

    /// <summary>
    /// Gets the position of the ship.
    /// </summary>
    IPosition3D Position { get; }
}
