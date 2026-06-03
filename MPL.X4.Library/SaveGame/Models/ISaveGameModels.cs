namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines the behaviour of save game models.
/// </summary>
public interface ISaveGameModels
{
    /// <summary>
    /// Gets the economy log from the save game.
    /// </summary>
    IEconomyLogModel EconomyLog { get; }

    /// <summary>
    /// Gets the universe from the save game.
    /// </summary>
    IUniverseModel Universe { get; }
}
