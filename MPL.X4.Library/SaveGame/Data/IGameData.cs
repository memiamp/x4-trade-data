namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a game data.
/// </summary>
public interface IGameData
{
    /// <summary>
    /// Gets the modified state of the game.
    /// </summary>
    int? Modified { get; }

    /// <summary>
    /// Gets the game time.
    /// </summary>
    double Time { get; }
}
