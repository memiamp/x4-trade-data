namespace MPL.X4;

/// <summary>
/// An interface that defines a save game.
/// </summary>
public interface ISaveGame
{
    /// <summary>
    /// Gets the universe from the save game.
    /// </summary>
    IUniverse Universe { get; }
}
