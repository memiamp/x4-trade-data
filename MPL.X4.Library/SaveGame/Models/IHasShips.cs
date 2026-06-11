namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines an element that has ships.
/// </summary>
public interface IHasShips
{
    /// <summary>
    /// Gets the ships.
    /// </summary>
    IShipModelList Ships { get; }
}
