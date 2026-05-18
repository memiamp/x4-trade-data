namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a ship.
/// </summary>
public interface IShipModel : IHasIsKnown, IHasTransform, IModelWithId
{
    /// <summary>
    /// Gets the ship cargo.
    /// </summary>
    ICargoItemModelList Cargo { get; }
    
    /// <summary>
    /// Gets the class of the ship.
    /// </summary>
    ShipClass Class { get; }

    /// <summary>
    /// Gets the code of the ship.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the model of the ship.
    /// </summary>
    string Model { get; }

    /// <summary>
    /// Gets the modifications applied to the ship.
    /// </summary>
    IModificationModelList Modifications { get; }

    /// <summary>
    /// Gets the name of the ship (if any).
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// Gets the owner of the ship.
    /// </summary>
    string Owner { get; }
}
