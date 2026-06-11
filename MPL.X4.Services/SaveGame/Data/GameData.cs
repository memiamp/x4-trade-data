namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a game.
/// </summary>
internal class GameData : IGameData
{
    public override string ToString()
        => $"{Time}";

    public required int? Modified { get; init; }

    public required double Time { get; init; }
}
