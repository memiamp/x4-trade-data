namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a universe.
/// </summary>
public interface IUniverseModel
{
    /// <summary>
    /// Gets the sectors belonging to the universe.
    /// </summary>
    ISectorModelList Sectors { get; }
}
