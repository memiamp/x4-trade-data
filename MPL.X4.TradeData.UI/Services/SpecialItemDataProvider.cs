using MPL.X4.SaveGame.Models;
using MPL.X4.TradeData.UI.Models.SpecialItem;

namespace MPL.X4.TradeData.UI.Services;

/// <summary>
/// A class that implements a provider of special item data.
/// </summary>
internal class SpecialItemDataProvider : ISpecialItemDataProvider
{
    IEnumerable<ISpecialItem> ISpecialItemDataProvider.GetAbandonedBuildStorages()
        => ((ISpecialItemDataProvider)this).SaveGame?
                                                     .Universe
                                                     .Sectors
                                                     .SelectMany(sector => sector
                                                                                 .BuildStorages
                                                                                 .Select(x => new AbandonedBuildStorageSpecialItem(sector.Name, x)))
                                                     ?? [];

    IEnumerable<ISpecialItem> ISpecialItemDataProvider.GetAbandonedShips()
        => ((ISpecialItemDataProvider)this).SaveGame?
                                                     .Universe
                                                     .Sectors
                                                     .SelectMany(sector => sector
                                                                                 .Ships
                                                                                 .Where(x => x.CanBeCaptured)
                                                                                 .Select(x => new AbandonedShipSpecialItem(sector.Name, x)))
                                                     ?? [];

    IEnumerable<ISpecialItem> ISpecialItemDataProvider.GetLockboxes()
        => ((ISpecialItemDataProvider)this).SaveGame?
                                                     .Universe
                                                     .Sectors
                                                     .SelectMany(sector => sector
                                                                                 .Lockboxes
                                                                                 .Select(x => new LockboxSpecialItem(sector.Name, x)))
                                                     ?? [];

    IEnumerable<ISpecialItem> ISpecialItemDataProvider.GetTopBuildStorages(int count)
    {
        if (count < 1)
        {
            count = 1;
        }
        else if (count > 20)
        {
            count = 20;
        }

        return ((ISpecialItemDataProvider)this).SaveGame?
                                                         .Universe
                                                         .Sectors
                                                         .SelectMany(sector => sector.Stations.Select(station => new
                                                         {
                                                             SectorName = sector.Name,
                                                             station.BuildStorage,
                                                             TotalAmount = station.BuildStorage?.Cargo.TotalAmount ?? 0,
                                                             TotalValue = station.BuildStorage?.Cargo.TotalValue ?? 0,
                                                             WareCount = station.BuildStorage?.Cargo.Count ?? 0
                                                         }))
                                                         .Where(x => x.BuildStorage is not null)
                                                         .OrderByDescending(x => x.TotalValue)
                                                         .ThenBy(x => x.WareCount)
                                                         .Take(count)
                                                         .Select((x, i) => new TopBuildStorageSpecialItem(i + 1, x.SectorName, x.BuildStorage!))
                                                         ?? [];
    }

    ISaveGameModels? ISpecialItemDataProvider.SaveGame { get; set; }
}
