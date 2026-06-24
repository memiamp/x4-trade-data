using MPL.X4.GameResources.Models.Services;

namespace MPL.X4.SaveGame.Models.Services;

/// <summary>
/// A class that implements extensions methods to an <see cref="ISectorModel"/>.
/// </summary>
public static class SectorModelExtensions
{
    /// <summary>
    /// Gets an enumeration containing all ships in the sector from all sources.
    /// </summary>
    /// <param name="source">An <see cref="ISectorModel"/> that is the sector.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="IShipModel"/> that is the result.</returns>
    public static IEnumerable<IShipModel> GetAllShips(this ISectorModel source)
    {
        // All ships consists of:
        // - All sector ships (from zones and highways)
        // - All station ships
        // - All build storage ships

        // Sector ships
        var sectorShips = source.Ships;

        // Station ships (docked)
        var stations = source.Stations;
        var stationShips = stations.SelectMany(x => x.Ships);

        // Station build storage ships (docked)
        var buildStorages = stations
                                    .Where(x => x.BuildStorage is not null)
                                    .Select(x => x.BuildStorage!);
        var stationBuildStorageShips = buildStorages.SelectMany(x => x.Ships);

        // Sector build storage (unassigned to a station) ships
        var sectorBuildStorageShips = source
                                            .BuildStorages
                                            .SelectMany(x => x.Ships);

        var ships = sectorShips
                               .Concat(stationShips)
                               .Concat(stationBuildStorageShips)
                               .Concat(sectorBuildStorageShips);

        return ships.Flatten();
    }

    /// <summary>
    /// Gets an indication of whether the specified <paramref name="source"/> is player-owned.
    /// </summary>
    /// <param name="source">A nullable <see cref="ISectorModel"/> that is the source to evaluate.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    public static bool IsPlayerOwned(this ISectorModel source)
        => source.Owner.IsPlayerOwned();
}
