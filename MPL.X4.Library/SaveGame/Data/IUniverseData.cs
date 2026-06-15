namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a universe.
/// </summary>
public interface IUniverseData
{
    /// <summary>
    /// Gets the galaxy belonging to the universe.
    /// </summary>
    IGalaxyData Galaxy { get; }
}
