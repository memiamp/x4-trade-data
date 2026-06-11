namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a ship.
/// </summary>
public interface IShipData : IHasIsKnown, IHasState, IHasTransform
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
    /// Gets the engine modification, if any, on the ship.
    /// </summary>
    IEngineModificationData? EngineModification { get; }

    /// <summary>
    /// Gets the identifier of the ship.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the macro of the ship.
    /// </summary>
    string Macro { get; }

    /// <summary>
    /// Gets the name of the ship.
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// Gets the owner of the ship.
    /// </summary>
    string Owner { get; }

    /// <summary>
    /// Gets the paint modification, if any, on the ship.
    /// </summary>
    IPaintModificationData? PaintModification { get; }

    /// <summary>
    /// Gets the shield modification, if any, on the ship.
    /// </summary>
    IShieldModificationData? ShieldModification { get; }

    /// <summary>
    /// Gets the ship modification, if any, on the ship.
    /// </summary>
    IShipModificationData? ShipModification { get; }

    /// <summary>
    /// Gets the ships docked at the ship.
    /// </summary>
    IEnumerable<IShipData> Ships { get; }

    /// <summary>
    /// Gets the weapon modifications on the ship.
    /// </summary>
    IEnumerable<IWeaponModificationData> WeaponModifications { get; }
}
