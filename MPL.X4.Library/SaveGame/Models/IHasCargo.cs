namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines an element that has cargo.
/// </summary>
public interface IHasCargo
{
    /// <summary>
    /// Gets the cargo.
    /// </summary>
    ICargoItemModelList Cargo { get; }
}
