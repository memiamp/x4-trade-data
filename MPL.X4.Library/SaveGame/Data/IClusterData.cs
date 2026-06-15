namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a cluster.
/// </summary>
public interface IClusterData : IHasIsKnown
{
    /// <summary>
    /// Gets the code of the cluster.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the highways in the cluster.
    /// </summary>
    IEnumerable<IHighwayData> Highways { get; }

    /// <summary>
    /// Gets the identifier of the cluster.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the macro of the cluster.
    /// </summary>
    string Macro { get; }

    /// <summary>
    /// Gets the sectors belonging to the cluster.
    /// </summary>
    IEnumerable<ISectorData> Sectors { get; }
}
