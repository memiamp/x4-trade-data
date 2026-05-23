using MPL.X4.GameResources.Models;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a collectable drop.
/// </summary>
public interface ICollectableDropModel : IHasIsKnown, IHasTransform, IModelWithId
{
    /// <summary>
    /// Gets the amount of the item.
    /// </summary>
    int Amount { get; }

    /// <summary>
    /// Gets an indication of whether the collectable drop is a wreck.
    /// </summary>
    bool IsWreck { get; }

    /// <summary>
    /// Gets the name of the collectable drop.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the type of the collectable drop.
    /// </summary>
    DropType Type { get; }

    /// <summary>
    /// Gets the ware type of the collectable drop.
    /// </summary>
    WareType WareType { get; }
}
