using Microsoft.Extensions.Logging;
using MPL.X4.Catalog;
using MPL.X4.Catalog.Services;
using MPL.X4.Imports.XmlPatch;
using MPL.X4.Services.Xml;

namespace MPL.X4.GameResources.Data.Services;

/// <summary>
/// A class that implements a game resource data loader.
/// </summary>
/// <param name="catalogFileReader">An <see cref="ICatalogFileReader"/> that is the catalog file reader to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="resourceDataParser">An <see cref="IGameResourceDataParser"/> that is the game resource data parser to use.</param>
/// <param name="xmlPatchService">An <see cref="IXmlPatchService"/> that is the XML patch service to use.</param>
/// <param name="xmlReaderWrapperFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XML reader factory to use.</param>
internal class GameResourceDataLoader(
                                      ICatalogFileReader catalogFileReader,
                                      ILogger<GameResourceDataLoader> logger,
                                      IGameResourceDataParser resourceDataParser,
                                      IXmlPatchService xmlPatchService,
                                      IXmlReaderWrapperFactory xmlReaderWrapperFactory)
    : IGameResourceDataLoader
{
    private static Dictionary<string, IEnumerable<ICatalogIndexEntry>> FilterIndexEntries(IEnumerable<ICatalogIndex> index, Func<ICatalogIndexEntry, bool> predicate)
        => index
                .Select(x =>
                {
                    var filtered = x.Entries
                                            .Where(x => x.Size > 0)
                                            .Where(predicate)
                                            .ToList();

                    return new
                    {
                        x.DataFilePath,
                        Entries = filtered as IEnumerable<ICatalogIndexEntry>
                    };
                })
                .Where(x => x.Entries.Any())
                .ToDictionary(
                              x => x.DataFilePath,
                              x => x.Entries);

    async Task<string?> MergeIndexEntriesToXml(IEnumerable<ICatalogIndex> index, Func<ICatalogIndexEntry, bool> predicate, bool throwOnNoData = true)
    {
        string? returnValue = null;

        var entries = FilterIndexEntries(index, predicate);
        await foreach (var file in catalogFileReader.ReadTextFiles(entries))
        {
            if (returnValue is null)
            {
                returnValue = file;
            }
            else
            {
                returnValue = xmlPatchService.PatchToString(returnValue, file);
            }
        }

        if (throwOnNoData &&
            returnValue is null)
        {
            throw new ArgumentException("The requested merge resulted in no data", nameof(index));
        }

        return returnValue;
    }

    async Task<IColourResourceData> IGameResourceDataLoader.LoadColourResourcesFromCatalogs(string catalogsFilePath)
    {
        logger.LogInformation("Loading colour data from catalogs at {CatalogsFilePath}", catalogsFilePath);

        var entries = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.ColourXml, Constants.CatalogFile.FileExtensions.XmlData, true);

        return await ((IGameResourceDataLoader)this).LoadColourResourcesFromIndex(entries);
    }

    async Task<IColourResourceData> IGameResourceDataLoader.LoadColourResourcesFromIndex(IEnumerable<ICatalogIndex> index)
    {
        var colours = new ColourDataDictionary();
        var mappings = new MappingDataDictionary();

        logger.LogInformation("Loading colour data from supplied index");

        var entries = FilterIndexEntries(index, x => x.FilePath.Contains(Constants.CatalogFile.FileName.ColourXml, StringComparison.OrdinalIgnoreCase));
        await foreach (var file in catalogFileReader.ReadTextFiles(entries))
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(file);

            var data = await resourceDataParser.ReadColourResources(reader);

            colours.Merge(data.Colours);
            mappings.Merge(data.Mappings);
        }

        return new ColourResourceData
        {
            Colours = colours,
            Mappings = mappings
        };
    }

    async Task<IFactionDataDictionary> IGameResourceDataLoader.LoadFactionsFromCatalogs(string catalogsFilePath)
    {
        logger.LogInformation("Loading faction data from catalogs at {CatalogsFilePath}", catalogsFilePath);

        var entries = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.FactionsXml, Constants.CatalogFile.FileExtensions.XmlData, true);

        return await ((IGameResourceDataLoader)this).LoadFactionsFromIndex(entries);
    }

    async Task<IFactionDataDictionary> IGameResourceDataLoader.LoadFactionsFromIndex(IEnumerable<ICatalogIndex> index)
    {
        logger.LogInformation("Loading faction data from supplied index");

        var file = await MergeIndexEntriesToXml(index, x => x.FilePath.Contains(Constants.CatalogFile.FileName.FactionsXml, StringComparison.OrdinalIgnoreCase));
        if (file is not null)
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(file);

            return await resourceDataParser.ReadFactions(reader);
        }

        return new FactionDataDictionary();
    }

    async Task<IMacroNameResourceDataDictionary> IGameResourceDataLoader.LoadLandmarkNamesFromCatalogs(string catalogsFilePath)
    {
        logger.LogInformation("Loading landmark names from catalogs at {CatalogsFilePath}", catalogsFilePath);

        var entries = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.LandmarkMacros, Constants.CatalogFile.FileExtensions.XmlData, true);

        return await ((IGameResourceDataLoader)this).LoadLandmarkNamesFromIndex(entries);
    }

    async Task<IMacroNameResourceDataDictionary> IGameResourceDataLoader.LoadLandmarkNamesFromIndex(IEnumerable<ICatalogIndex> index)
    {
        var returnValue = new MacroNameResourceDataDictionary();

        logger.LogInformation("Loading landmark names from supplied index");

        var entries = FilterIndexEntries(index, x => x.FilePath.Contains(Constants.CatalogFile.FileName.LandmarkMacros, StringComparison.OrdinalIgnoreCase));
        await foreach (var file in catalogFileReader.ReadTextFiles(entries))
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(file);

            var data = await resourceDataParser.ReadLandmarkNames(reader);

            returnValue.Merge(data);
        }

        return returnValue;
    }

    async Task<IOffsetDataDictionary> IGameResourceDataLoader.LoadOffsetsFromCatalogs(string catalogsFilePath)
    {
        logger.LogInformation("Loading offset data from catalogs at {CatalogsFilePath}", catalogsFilePath);

        var entries = await catalogFileReader.ParseIndexes(catalogsFilePath, "maps/", Constants.CatalogFile.FileExtensions.XmlData, true);

        return await ((IGameResourceDataLoader)this).LoadOffsetsFromIndex(entries);
    }

    async Task<IOffsetDataDictionary> IGameResourceDataLoader.LoadOffsetsFromIndex(IEnumerable<ICatalogIndex> index)
    {
        OffsetDataDictionary returnValue = [];
    
        logger.LogInformation("Loading offset data from supplied index");

        var entries = FilterIndexEntries(index, x => x.FilePath.StartsWith("maps/", StringComparison.OrdinalIgnoreCase));
        await foreach (var file in catalogFileReader.ReadTextFiles(entries))
        {
            // Need to have a think how best to perform diffs on map files as there are many of them
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(file);

            var data = await resourceDataParser.ReadOffsets(reader);

            returnValue.Merge(data);
        }

        return returnValue;
    }

    async Task<IMacroNameResourceDataDictionary> IGameResourceDataLoader.LoadSectorNamesFromCatalogs(string catalogsFilePath)
    {
        logger.LogInformation("Loading sector names from catalogs at {CatalogsFilePath}", catalogsFilePath);

        var entries = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.MapDefinitionXml, Constants.CatalogFile.FileExtensions.XmlData, true);

        return await ((IGameResourceDataLoader)this).LoadSectorNamesFromIndex(entries);
    }

    async Task<IMacroNameResourceDataDictionary> IGameResourceDataLoader.LoadSectorNamesFromIndex(IEnumerable<ICatalogIndex> index)
    {
        var returnValue = new MacroNameResourceDataDictionary();

        logger.LogInformation("Loading sector names from supplied index");

        var entries = FilterIndexEntries(index, x => x.FilePath.Contains(Constants.CatalogFile.FileName.MapDefinitionXml, StringComparison.OrdinalIgnoreCase));
        await foreach (var file in catalogFileReader.ReadTextFiles(entries))
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(file);

            var data = await resourceDataParser.ReadSectorNames(reader);

            returnValue.Merge(data);
        }

        return returnValue;
    }

    async Task<IMacroNameResourceDataDictionary> IGameResourceDataLoader.LoadShipModelsFromCatalogs(string catalogsFilePath)
    {
        logger.LogInformation("Loading ship models from catalogs at {CatalogsFilePath}", catalogsFilePath);

        var entriesL = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.ShipMacrosL, Constants.CatalogFile.FileExtensions.XmlData, true);
        var entriesM = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.ShipMacrosM, Constants.CatalogFile.FileExtensions.XmlData, true);
        var entriesS = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.ShipMacrosS, Constants.CatalogFile.FileExtensions.XmlData, true);
        var entriesXL = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.ShipMacrosXL, Constants.CatalogFile.FileExtensions.XmlData, true);
        var entriesXS = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.ShipMacrosXS, Constants.CatalogFile.FileExtensions.XmlData, true);

        var entries = entriesL
                              .Concat(entriesM)
                              .Concat(entriesS)
                              .Concat(entriesXL)
                              .Concat(entriesXS);

        return await ((IGameResourceDataLoader)this).LoadShipModelsFromIndex(entries);
    }

    async Task<IMacroNameResourceDataDictionary> IGameResourceDataLoader.LoadShipModelsFromIndex(IEnumerable<ICatalogIndex> index)
    {
        MacroNameResourceDataDictionary returnValue = [];
        
        logger.LogInformation("Loading ship models from supplied index");

        var entries = FilterIndexEntries(index, x => Constants.CatalogFile.FileName.ShipMacros.Any(y => x.FilePath.Contains(y, StringComparison.OrdinalIgnoreCase)));
        await foreach (var file in catalogFileReader.ReadTextFiles(entries))
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(file);

            var data = await resourceDataParser.ReadShipModels(reader);

            returnValue.Merge(data);
        }

        return returnValue;
    }

    async Task<ITextResourcePageDictionary> IGameResourceDataLoader.LoadTextResourcesFromCatalogs(string catalogsFilePath)
    {
        var languageFile = await catalogFileReader.ReadTextFile(
                                                                catalogsFilePath,
                                                                Constants.CatalogFile.FileId.TextResources,
                                                                Constants.CatalogFile.FileName.EnglishTextResource);

        using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(languageFile);

        return await resourceDataParser.ReadTextResources(reader);
    }

    async Task<IMacroNameResourceDataDictionary> IGameResourceDataLoader.LoadWareNamesFromCatalogs(string catalogsFilePath)
    {
        logger.LogInformation("Loading ware names from catalogs at {CatalogsFilePath}", catalogsFilePath);

        var entries = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.WareDefinition, Constants.CatalogFile.FileExtensions.XmlData, true);

        return await ((IGameResourceDataLoader)this).LoadWareNamesFromIndex(entries);
    }

    async Task<IMacroNameResourceDataDictionary> IGameResourceDataLoader.LoadWareNamesFromIndex(IEnumerable<ICatalogIndex> index)
    {
        logger.LogInformation("Loading ware names from supplied index");

        var file = await MergeIndexEntriesToXml(index, x => x.FilePath.Contains(Constants.CatalogFile.FileName.WareDefinition, StringComparison.OrdinalIgnoreCase));
        if (file is not null)
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(file);

            return await resourceDataParser.ReadWareNames(reader);
        }

        return new MacroNameResourceDataDictionary();
    }
}
