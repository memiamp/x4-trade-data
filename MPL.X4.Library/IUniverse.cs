namespace MPL.X4;

/// <summary>
/// An interface that defines a universe.
/// </summary>
public interface IUniverse
{
    /// <summary>
    /// Gets the sectors belonging to the universe.
    /// </summary>
    IEnumerable<ISector> Sectors { get; }
}
