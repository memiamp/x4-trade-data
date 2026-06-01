namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a cargo item.
/// </summary>
public interface ICargoItemModel : IModelWithId
{
    /// <summary>
    /// Gets the amount of the item.
    /// </summary>
    int Amount { get; }

    /// <summary>
    /// Gets the name of the cargo item.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the value of the item (based on average price).
    /// </summary>
    int Value { get; }
}
