namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a save game.
/// </summary>
public interface ISaveGameData
{
    /// <summary>
    /// Gets the economy log from the save game.
    /// </summary>
    IEconomyLogData EconomyLog { get; }

    /// <summary>
    /// Gets the universe from the save game.
    /// </summary>
    IUniverseData Universe { get; }
}
