namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a sector.
/// </summary>
internal class SectorData : ISectorData
{
    public override string ToString()
        => $"{Owner} {Macro} {Code} - Stations {Stations.Count()} Ships {Ships.Count()} IsKnown {IsKnown} - {Id}";

    public required string Code { get; init; }

    public required IEnumerable<IGateData> Gates { get; init; }
 
    public required string Id { get; init; }

    public required bool IsKnown { get; init; }

    public required IEnumerable<ILockboxData> Lockboxes { get; init; }

    public required string Macro { get; init; }

    public required string Owner { get; init; }

    public required IEnumerable<IShipData> Ships { get; init; }

    public required IEnumerable<IStationData> Stations { get; init; }
}
