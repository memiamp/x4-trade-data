namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of save game information.
/// </summary>
internal class InformationData : IInformationData
{
    public override string ToString()
        => $"{Save} - {Player}";

    public required IGameData Game { get; init; }

    public required IPlayerData Player { get; init; }

    public required ISaveData Save { get; init; }
}
