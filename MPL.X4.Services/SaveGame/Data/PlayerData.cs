namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a player.
/// </summary>
internal class PlayerData : IPlayerData
{
    public override string ToString()
        => $"{Name} {Money} - {Location}";

    public required ITextResourceReference Location { get; init; }

    public required long Money { get; init; }

    public required string Name { get; init; }
}
