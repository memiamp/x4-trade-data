using Microsoft.Extensions.Logging;
using MPL.X4.Catalog;
using MPL.X4.Catalog.Services;
using MPL.X4.Services.Xml;

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

            colours.Merge(data.Colours);
            mappings.Merge(data.Mappings);
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

    private async Task<ISectorNameDataDictionary> LoadSectorNamesInternal(IEnumerable<ICatalogIndexEntry> index)
    {
        SectorNameDataDictionary returnValue = [];

        var files = catalogFileReader.ReadTextFiles(index);
        await foreach (var file in files)
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(file);

            var data = await resourceDataParser.ReadSectorNames(reader);

            returnValue.Merge(data);
        }

        return returnValue;
    }

    private async Task<IOffsetDataDictionary> LoadOffsetsInternal(IEnumerable<ICatalogIndexEntry> index)
    {
        OffsetDataDictionary returnValue = [];

        var files = catalogFileReader.ReadTextFiles(index);
        await foreach (var file in files)
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(file);

            var data = await resourceDataParser.ReadOffsets(reader);

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

    async Task<IOffsetDataDictionary> IGameResourceDataLoader.LoadOffsetsFromCatalogs(string catalogsFilePath)
    {
        logger.LogInformation("Loading offset data from catalogs at {CatalogsFilePath}", catalogsFilePath);

        var entries = await catalogFileReader.ParseIndexes(catalogsFilePath, "maps/", Constants.CatalogFile.FileExtensions.XmlData, true);

        return await LoadOffsetsInternal(entries);
    }

    async Task<IOffsetDataDictionary> IGameResourceDataLoader.LoadOffsetsFromIndex(IEnumerable<ICatalogIndexEntry> index)
    {
        logger.LogInformation("Loading offset data from supplied index");

        var entries = index.Where(x => x.FilePath.StartsWith("maps/", StringComparison.OrdinalIgnoreCase));

        return await LoadOffsetsInternal(entries);
    }

    async Task<ISectorNameDataDictionary> IGameResourceDataLoader.LoadSectorNamesFromCatalogs(string catalogsFilePath)
    {
        logger.LogInformation("Loading sector names from catalogs at {CatalogsFilePath}", catalogsFilePath);

        var entries = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.MapDefinitionXml, Constants.CatalogFile.FileExtensions.XmlData, true);
   
        return await LoadSectorNamesInternal(entries);
    }

    async Task<ISectorNameDataDictionary> IGameResourceDataLoader.LoadSectorNamesFromIndex(IEnumerable<ICatalogIndexEntry> index)
    {
        logger.LogInformation("Loading sector names from supplied index");

        var entries = index.Where(x => x.FilePath.Contains(Constants.CatalogFile.FileName.MapDefinitionXml, StringComparison.OrdinalIgnoreCase));

        return await LoadSectorNamesInternal(entries);
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
}
