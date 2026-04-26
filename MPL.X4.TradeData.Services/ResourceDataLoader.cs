using Microsoft.Extensions.Logging;
using MPL.X4.Services;

namespace MPL.X4.TradeData.Services;

/// <summary>
/// A class that implements a resource data service.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="resourceFileReader">An <see cref="IResourceFileReader"/> that is the resource file reader service.</param>
internal class ResourceDataLoader(
                                  ILogger<ResourceDataLoader> logger,
                                  IResourceFileReader resourceFileReader)
    : IResourceDataLoader
{
    private static Dictionary<int, string> ParseSectorNames(Dictionary<int, string> source)
        => source
                 .Where(x => (x.Key % 10) == 1) // Keys ending with 1 are sector names
                 .ToDictionary(
                               x => x.Key / 10,
                               x => ParseSectorName(x.Value));

    private static string ParseSectorName(string source)
    {
        var indexPos = source.IndexOf('(') + 1;
        return indexPos > 0
                            ? source[indexPos..^1]
                            : source;
    }

    async Task<IResourceData> IResourceDataLoader.LoadFrom(string sourcePath)
    {
        var results = await resourceFileReader.ReadPages(
                                                         sourcePath,
                                                         Constants.ResourceFile.PageId.Landmarks,
                                                         Constants.ResourceFile.PageId.SectorNames,
                                                         Constants.ResourceFile.PageId.StationNames,
                                                         Constants.ResourceFile.PageId.WareGroups,
                                                         Constants.ResourceFile.PageId.Wares);

        var landmarks = results[Constants.ResourceFile.PageId.Landmarks];
        var stationNames = results[Constants.ResourceFile.PageId.StationNames];

        var sectorNames = ParseSectorNames(results[Constants.ResourceFile.PageId.SectorNames]);
        results[Constants.ResourceFile.PageId.SectorNames] = sectorNames;

        return new ResourceData(results)
        {
            Landmarks = landmarks,
            SectorNames = sectorNames,
            StationNames = stationNames
        };
    }
}
