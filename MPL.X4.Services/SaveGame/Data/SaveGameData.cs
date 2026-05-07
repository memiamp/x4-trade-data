namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a save game.
/// </summary>
internal class SaveGame : ISaveGame
{
    public required IUniverse Universe { get; init; }
}
