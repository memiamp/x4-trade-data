using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models;

/// <summary>
/// A class that implements an item for the sector list.
/// </summary>
internal class SectorListItem
{
    /// <summary>
    /// Creates an instance of the <see cref="SectorListItem"/> class with the specified parameters.
    /// </summary>
    /// <param name="sector">An <see cref="ISectorModel"/> that is the sector.</param>
    internal SectorListItem(
                            ISectorModel sector)
    {
        Sector = sector;

        AbandonedShipCount = sector.Ships.Count(x => x.CanBeCaptured);
        BackColour = sector.Owner?.Colour?.Colour ?? Constants.Colours.Unset;
        BuildStorageCount = sector.BuildStorages.Count;
        DropCount = sector.CollectableDrops.Count;
        LockboxCount = sector.Lockboxes.Count;
        Name = sector.Name;
        Owner = sector.Owner;
        OwnerName = string.IsNullOrWhiteSpace(sector.Owner?.Name)
                                                                  ? Constants.Owner.Unowned
                                                                  : sector.Owner.Name;
        ShipCount = sector.Ships.Count(x => !x.IsWreck);
        ShipwreckCount = sector.Ships.Count(x => x.IsWreck);
        StationCount = sector.Stations.Count(x => !x.IsWreck);
        StationWreckCount = sector.Stations.Count(x => x.IsWreck);
    }

    /// <summary>
    /// Gets the count of abandoned ships in the sector.
    /// </summary>
    internal int AbandonedShipCount { get; private set; }

    /// <summary>
    /// Gets the back colour.
    /// </summary>
    internal Color BackColour { get; private set; }

    /// <summary>
    /// Gets the count of build storages in the sector.
    /// </summary>
    internal int BuildStorageCount { get; private set; }

    /// <summary>
    /// Gets the count of drops in the sector.
    /// </summary>
    internal int DropCount { get; private set; }

    /// <summary>
    /// Gets the count of lockboxes in the sector.
    /// </summary>
    internal int LockboxCount { get; private set; }

    /// <summary>
    /// Gets the sector name.
    /// </summary>
    internal string Name { get; private set; }

    /// <summary>
    /// Gets the sector owner.
    /// </summary>
    internal IFactionModel? Owner { get; private set; }

    /// <summary>
    /// Gets the name of the sector owner.
    /// </summary>
    internal string OwnerName { get; private set; }

    /// <summary>
    /// Gets the sector.
    /// </summary>
    internal ISectorModel Sector { get; private set; }

    /// <summary>
    /// Gets the count of ships in the sector.
    /// </summary>
    internal int ShipCount { get; private set; }

    /// <summary>
    /// Gets the count of wrecked ships in the sector.
    /// </summary>
    internal int ShipwreckCount { get; private set; }

    /// <summary>
    /// Gets the count of stations in the sector.
    /// </summary>
    internal int StationCount { get; private set; }

    /// <summary>
    /// Gets the count of wrecked stations in the sector.
    /// </summary>
    internal int StationWreckCount { get; private set; }
}
