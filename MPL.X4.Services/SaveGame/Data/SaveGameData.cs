namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a save game.
/// </summary>
internal class SaveGameData : ISaveGameData
{
    public required IUniverseData Universe { get; init; }
}
