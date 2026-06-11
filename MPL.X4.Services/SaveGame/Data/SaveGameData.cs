namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a save game.
/// </summary>
internal class SaveGameData : ISaveGameData
{
    public required IEconomyLogData EconomyLog { get; init; }

    public required IInformationData Information { get; init; }

    public required IUniverseData Universe { get; init; }
}
