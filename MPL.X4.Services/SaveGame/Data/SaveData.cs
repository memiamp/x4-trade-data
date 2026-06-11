namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a save.
/// </summary>
internal class SaveData : ISaveData
{
    public override string ToString()
        => $"{Name} - {Date}";

    public required decimal Date { get; init; }

    public required string Name { get; init; }
}
