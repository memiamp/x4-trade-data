namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a galaxy.
/// </summary>
public interface IGalaxyData
{
    /// <summary>
    /// Gets the clusters belonging to the galaxy.
    /// </summary>
    IEnumerable<IClusterData> Clusters { get; }

    /// <summary>
    /// Gets the code of the galaxy.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the identifier of the galaxy.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the macro of the galaxy.
    /// </summary>
    string Macro { get; }
}
