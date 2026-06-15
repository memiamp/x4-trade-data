namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a cluster.
/// </summary>
internal class ClusterData : IClusterData
{
    public override string ToString()
        => $"{Macro} ({Code}) - Sectors {Sectors.Count()} - {Id}";

    public required string Code { get; init; }

    public required IEnumerable<IHighwayData> Highways { get; init; }

    public required string Id { get; init; }
    
    public required bool IsKnown { get; init; }

    public required string Macro { get; init; }

    public required IEnumerable<ISectorData> Sectors { get; init; }
}
