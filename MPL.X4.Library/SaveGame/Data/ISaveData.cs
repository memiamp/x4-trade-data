namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a save data.
/// </summary>
public interface ISaveData
{
    /// <summary>
    /// Gets the save game date.
    /// </summary>
    decimal Date { get; }

    /// <summary>
    /// Gets the save game name.
    /// </summary>
    string Name { get; }
}
