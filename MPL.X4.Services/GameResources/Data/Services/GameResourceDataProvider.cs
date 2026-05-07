using Microsoft.Extensions.Logging;
using MPL.X4.Catalog.Services;

namespace MPL.X4.GameResources.Data.Services;

/// <summary>
/// A class that implements a resource data provider.
/// </summary>
/// <param name="catalogFileReader">An <see cref="ICatalogFileReader"/> that is the catalog file reader to use.</param>
/// <param name="gameResourceDataAccessor">An <see cref="IGameResourceDataAccessor"/> that is the resource data accessor to use.</param>
/// <param name="gameResourceDataLoader">An <see cref="IGameResourceDataLoader"/> that is the resource data loader service.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal partial class GameResourceDataProvider(
                                                ICatalogFileReader catalogFileReader,
                                                IGameResourceDataAccessor gameResourceDataAccessor,
                                                IGameResourceDataLoader gameResourceDataLoader,
                                                ILogger<GameResourceDataProvider> logger)
    : IGameResourceDataProvider
{
    async Task<IGameResourceData> IGameResourceDataProvider.LoadFromCatalog(string catalogFilePath)
    {
        var index = await catalogFileReader.ParseIndexes(catalogFilePath, null, Constants.CatalogFile.FileExtensions.XmlData, true);

        logger.LogInformation("Found {FileCount} index files", index.Count());

        var colours = await gameResourceDataLoader.LoadColourResourcesFromIndex(index);
        var factions = await gameResourceDataLoader.LoadFactionsFromIndex(index);
        var sectorNameMacros = await gameResourceDataLoader.LoadSectorNameMacroMapFromIndex(index);
        var textResource = await gameResourceDataLoader.LoadTextResourcesFromCatalogs(catalogFilePath);
        var zoneOffsets = await gameResourceDataLoader.LoadZoneOffsetsFromIndex(index);

        gameResourceDataAccessor.Instance = new GameResourceData
        {
            Colours = colours,
            Factions = factions,
            SectorMacros = sectorNameMacros,
            Text = textResource,
            ZoneOffsets = zoneOffsets
        };

        return gameResourceDataAccessor.Instance;
    }
}
