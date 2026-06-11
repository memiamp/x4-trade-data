namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of save game information.
/// </summary>
public interface IInformationData
{
    /// <summary>
    /// Gets the game information.
    /// </summary>
    IGameData Game { get; }

    /// <summary>
    /// Gets the player information.
    /// </summary>
    IPlayerData Player { get; }

    /// <summary>
    /// Gets the save information.
    /// </summary>
    ISaveData Save { get; }
}
