namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines the behaviour of save game models.
/// </summary>
public interface ISaveGameModels
{
    /// <summary>
    /// Gets the universe from the save game.
    /// </summary>
    IUniverseModel Universe { get; }
}
