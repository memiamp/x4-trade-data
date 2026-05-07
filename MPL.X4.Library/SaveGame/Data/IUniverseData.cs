namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a universe.
/// </summary>
public interface IUniverseData
{
    /// <summary>
    /// Gets the sectors belonging to the universe.
    /// </summary>
    IEnumerable<ISectorData> Sectors { get; }
}
