using Microsoft.Extensions.Logging;
using MPL.X4.Data;
using MPL.X4.Services.Data;

namespace MPL.X4.Services;

/// <summary>
/// A class that implements a resource data loader.
/// </summary>
/// <param name="catalogFileReader">An <see cref="ICatalogFileReader"/> that is the catalog file reader to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="resourceFileReader">An <see cref="IResourceFileReader"/> that is the resource file reader service.</param>
/// <param name="xmlReaderWrapperFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XML reader factory to use.</param>
internal class ResourceDataLoader(
                                  ICatalogFileReader catalogFileReader,
                                  ILogger<ResourceDataLoader> logger,
                                  IResourceFileReader resourceFileReader,
                                  IXmlReaderWrapperFactory xmlReaderWrapperFactory)
    : IResourceDataLoader
{
    async Task<IFactionsData> IResourceDataLoader.LoadFactionsFromCatalogs(string catalogsFilePath)
    {
        FactionsData returnValue = [];

        logger.LogInformation("Loading faction data from catalogs at {CatalogsFilePath}", catalogFileReader);

        var factionFileEntries = await catalogFileReader.ParseIndexes(catalogsFilePath, Constants.CatalogFile.FileName.FactionsXml, Constants.CatalogFile.FileExtensions.XmlData, true);

        var factionFiles = catalogFileReader.ReadTextFiles(factionFileEntries);
        await foreach (var factionFile in factionFiles)
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(factionFile);

            var factions = await resourceFileReader.ReadFactions(reader);

            returnValue.Merge(factions);
        }

        return returnValue;
    }
}
