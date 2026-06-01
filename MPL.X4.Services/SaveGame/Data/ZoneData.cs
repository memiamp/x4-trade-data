namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model for a zone.
/// </summary>
internal class ZoneData : HasTransformBase, IZoneData
{
    public override string ToString()
        => $"{Code} {Transform} - Gates {Gates.Count()} Lockboxes {Lockboxes.Count()} Ships {Ships.Count()} Stations {Stations.Count()} - {Id}";

    public required IEnumerable<IBuildStorageData> BuildStorages { get; init; }
   
    public required string Code { get; init; }

    public required IEnumerable<ICollectableAmmoData> CollectableAmmos { get; init; }

    public required IEnumerable<ICollectableWareData> CollectableWares { get; init; }

    public required IEnumerable<IGateData> Gates { get; init; }

    public required string Id { get; init; }

    public required bool IsKnown { get; init; }
  
    public required IEnumerable<ILockboxData> Lockboxes { get; init; }

    public required string Macro { get; init; }

    public required IEnumerable<IShipData> Ships { get; init; }

    public required IEnumerable<IStationData> Stations { get; init; }
}
