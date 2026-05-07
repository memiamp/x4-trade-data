using Microsoft.Extensions.Logging;
using MPL.X4.Catalog;
using MPL.X4.Catalog.Services;

namespace MPL.X4.GameResources.Data.Services;

/// <summary>
/// A class that implements a game resource data loader.
/// </summary>
/// <param name="catalogFileReader">An <see cref="ICatalogFileReader"/> that is the catalog file reader to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="resourceDataParser">An <see cref="IGameResourceDataParser"/> that is the game resource data parser to use.</param>
/// <param name="xmlReaderWrapperFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XML reader factory to use.</param>
internal class GameResourceDataLoader(
                                      ICatalogFileReader catalogFileReader,
                                      ILogger<GameResourceDataLoader> logger,
                                      IGameResourceDataParser resourceDataParser,
                                      IXmlReaderWrapperFactory xmlReaderWrapperFactory)
    : IGameResourceDataLoader
{
    private async Task<IColourResourceData> LoadColourResourcesInternal(IEnumerable<ICatalogIndexEntry> index)
    {
        var colours = new ColourDataDictionary();
        var mappings = new MappingDataDictionary();
     
        var files = catalogFileReader.ReadTextFiles(index);
        await foreach (var file in files)
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(file);

            var data = await resourceDataParser.ReadColourResources(reader);

            foreach (var kvp in data.Colours)
            {
                colours[kvp.Key] = kvp.Value;
            }

            foreach (var kvp in data.Mappings)
            {
                mappings[kvp.Key] = kvp.Value;
            }
        }

        return new ColourResourceData
        {
            Colours = colours,
            Mappings = mappings
        };
    }

    private async Task<IFactionDataDictionary> LoadFactionsInternal(IEnumerable<ICatalogIndexEntry> index)
    {
        FactionDataDictionary returnValue = [];
     
        var files = catalogFileReader.ReadTextFiles(index);
        await foreach (var file in files)
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(file);

            var data = await resourceDataParser.ReadFactions(reader);

            returnValue.Merge(data);
        }

        return returnValue;
    }

    private async Task<Dictionary<string, string>> LoadSectorNameMacroMapInternal(IEnumerable<ICatalogIndexEntry> index)
    {
        var returnValue = new Dictionary<string, string>();

        var mapDefinitions = catalogFileReader.ReadTextFiles(index);
        await foreach (var mapDefinition in mapDefinitions)
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(mapDefinition);

            var data = await resourceDataParser.ReadSectorMacroMap(reader);

            foreach (var (key, value) in data)
            {
                returnValue[key] = value;
            }
        }

        return returnValue;
    }

    private async Task<IZoneOffsetDataDictionary> LoadZoneOffsetsInternal(IEnumerable<ICatalogIndexEntry> index)
    {
        ZoneOffsetDataDictionary returnValue = [];

        var files = catalogFileReader.ReadTextFiles(index);
        await foreach (var file in files)
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(file);

            var data = await resourceDataParser.ReadZoneOffsets(reader);

            returnValue.Merge(data);
        }

        return returnValue;
    }

    async Task<IColourResourceData> IGameResourceDataLoader.LoadColourResourcesFromCatalogs(string catalogsFilePath)
    {
        logger.LogInformation("Loading colour data from catalogs at {CatalogsFilePath}", catalogsFilePath);
       
        var entries = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.ColourXml, Constants.CatalogFile.FileExtensions.XmlData, true);

        return await LoadColourResourcesInternal(entries);
    }

    async Task<IColourResourceData> IGameResourceDataLoader.LoadColourResourcesFromIndex(IEnumerable<ICatalogIndexEntry> index)
    {
        logger.LogInformation("Loading colour data from supplied index");

        var entries = index.Where(x => x.FilePath.Contains(Constants.CatalogFile.FileName.ColourXml, StringComparison.OrdinalIgnoreCase));
    
        return await LoadColourResourcesInternal(entries);
    }

    async Task<IFactionDataDictionary> IGameResourceDataLoader.LoadFactionsFromCatalogs(string catalogsFilePath)
    {
        logger.LogInformation("Loading faction data from catalogs at {CatalogsFilePath}", catalogsFilePath);

        var entries = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.FactionsXml, Constants.CatalogFile.FileExtensions.XmlData, true);
     
        return await LoadFactionsInternal(entries);
    }

    async Task<IFactionDataDictionary> IGameResourceDataLoader.LoadFactionsFromIndex(IEnumerable<ICatalogIndexEntry> index)
    {
        logger.LogInformation("Loading faction data from supplied index");

        var entries = index.Where(x => x.FilePath.Contains(Constants.CatalogFile.FileName.FactionsXml, StringComparison.OrdinalIgnoreCase));

        return await LoadFactionsInternal(entries);
    }

    async Task<Dictionary<string, string>> IGameResourceDataLoader.LoadSectorNameMacroMapFromCatalogs(string catalogsFilePath)
    {
        logger.LogInformation("Loading sector macro map from catalogs at {CatalogsFilePath}", catalogsFilePath);

        var entries = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.MapDefinitionXml, Constants.CatalogFile.FileExtensions.XmlData, true);
   
        return await LoadSectorNameMacroMapInternal(entries);
    }

    async Task<Dictionary<string, string>> IGameResourceDataLoader.LoadSectorNameMacroMapFromIndex(IEnumerable<ICatalogIndexEntry> index)
    {
        logger.LogInformation("Loading sector macro map from supplied index");

        var entries = index.Where(x => x.FilePath.Contains(Constants.CatalogFile.FileName.MapDefinitionXml, StringComparison.OrdinalIgnoreCase));

        return await LoadSectorNameMacroMapInternal(entries);
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

    async Task<IZoneOffsetDataDictionary> IGameResourceDataLoader.LoadZoneOffsetsFromCatalogs(string catalogsFilePath)
    {
        logger.LogInformation("Loading zone offset data from catalogs at {CatalogsFilePath}", catalogsFilePath);

        var entries = await catalogFileReader.ParseIndexes(catalogsFilePath, "maps/", Constants.CatalogFile.FileExtensions.XmlData, true);

        return await LoadZoneOffsetsInternal(entries);
    }

    async Task<IZoneOffsetDataDictionary> IGameResourceDataLoader.LoadZoneOffsetsFromIndex(IEnumerable<ICatalogIndexEntry> index)
    {
        logger.LogInformation("Loading zone offset data from supplied index");

        var entries = index.Where(x => x.FilePath.StartsWith("maps/", StringComparison.OrdinalIgnoreCase));

        return await LoadZoneOffsetsInternal(entries);
    }
}
