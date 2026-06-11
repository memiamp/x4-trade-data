namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a player data.
/// </summary>
public interface IPlayerData
{
    /// <summary>
    /// Gets the location of the player.
    /// </summary>
    ITextResourceReference Location { get; }

    /// <summary>
    /// Gets the money of the player.
    /// </summary>
    long Money { get; }

    /// <summary>
    /// Gets the name of the player.
    /// </summary>
    string Name { get; }
}
