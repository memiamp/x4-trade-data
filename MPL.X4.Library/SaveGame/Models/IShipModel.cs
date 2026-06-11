namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a ship.
/// </summary>
public interface IShipModel : IHasCode, IHasCargo, IHasIsKnown, IHasOwner, IHasShips, IHasTransform, IIsWreckable, IModelWithId
{
    /// <summary>
    /// Gets an indication of whether the ship can be captured.
    /// </summary>
    bool CanBeCaptured => IsAbandoned && !IsWreck;
   
    /// <summary>
    /// Gets the class of the ship.
    /// </summary>
    ShipClass Class { get; }

    /// <summary>
    /// Gets the engine modification, if any, on the ship.
    /// </summary>
    IEngineModificationModel? EngineModification { get; }

    /// <summary>
    /// Gets an indication of whether the ship is abandoned.
    /// </summary>
    bool IsAbandoned { get; }

    /// <summary>
    /// Gets the model of the ship.
    /// </summary>
    string Model { get; }

    /// <summary>
    /// Gets the name of the ship (if any).
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// Gets the paint modification, if any, on the ship.
    /// </summary>
    IPaintModificationModel? PaintModification { get; }

    /// <summary>
    /// Gets the shield modification, if any, on the ship.
    /// </summary>
    IShieldModificationModel? ShieldModification { get; }

    /// <summary>
    /// Gets the ship modification, if any, on the ship.
    /// </summary>
    IShipModificationModel? ShipModification { get; }

    /// <summary>
    /// Gets the weapon modifications on the ship.
    /// </summary>
    IEnumerable<IWeaponModificationModel> WeaponModifications { get; }
}
