namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a zone.
/// </summary>
public interface IZoneData : IHasTransform
{
    /// <summary>
    /// Gets the build storages in the zone.
    /// </summary>
    IEnumerable<IBuildStorageData> BuildStorages { get; }

    /// <summary>
    /// Gets the code of the zone.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the collectable ammos in the zone.
    /// </summary>
    IEnumerable<ICollectableAmmoData> CollectableAmmos { get; }

    /// <summary>
    /// Gets the collectable wares in the zone.
    /// </summary>
    IEnumerable<ICollectableWareData> CollectableWares { get; }

    /// <summary>
    /// Gets the gates in the zone.
    /// </summary>
    IEnumerable<IGateData> Gates { get; }

    /// <summary>
    /// Gets the identifier of the zone.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets an indication of whether the station is known to the player.
    /// </summary>
    bool IsKnown { get; }

    /// <summary>
    /// Gets the lockboxes in the zone.
    /// </summary>
    IEnumerable<ILockboxData> Lockboxes { get; }

    /// <summary>
    /// Gets the zone macro.
    /// </summary>
    string Macro { get; }

    /// <summary>
    /// Gets the ships in the zone.
    /// </summary>
    IEnumerable<IShipData> Ships { get; }

    /// <summary>
    /// Gets the stations in the zone.
    /// </summary>
    IEnumerable<IStationData> Stations { get; }
}
