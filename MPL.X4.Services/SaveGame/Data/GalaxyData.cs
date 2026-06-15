namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a galaxy.
/// </summary>
internal class GalaxyData : IGalaxyData
{
    public override string ToString()
        => $"{Macro} ({Code}) - Clusters {Clusters.Count()} - {Id}";

    public required IEnumerable<IClusterData> Clusters { get; init; }

    public required string Code { get; init; }

    public required string Id { get; init; }

    public required string Macro { get; init; }
}
