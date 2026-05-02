using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using MPL.X4.Services;

namespace MPL.X4.TradeData.Services;

/// <summary>
/// A class that implements a resource data service.
/// </summary>
/// <param name="catalogFileReader">An <see cref="ICatalogFileReader"/> that is the catalog file reader to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="resourceFileReader">An <see cref="IResourceFileReader"/> that is the resource file reader service.</param>
/// <param name="xmlReaderWrapperFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XML reader factory to use.</param>
internal partial class ResourceDataLoader(
                                          ICatalogFileReader catalogFileReader,
                                          ILogger<ResourceDataLoader> logger,
                                          IResourceFileReader resourceFileReader,
                                          IXmlReaderWrapperFactory xmlReaderWrapperFactory)
    : IResourceDataLoader
{
    [GeneratedRegex(@"(.*)\([^\(]*\)$", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex TextParenthesesRegex();

    [GeneratedRegex(@"\{[^}]+\}", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex TextPlaceholderRegex();

    private static Dictionary<int, string> GetMappedTextDictionary(Dictionary<int, Dictionary<int, string>> source, int pageId)
    {
        var returnValue = source[pageId];

        RecursiveMapNames(returnValue, source);

        return returnValue;
    }

    private static IEnumerable<ICatalogIndexEntry> GetXmlFilesOnly(IEnumerable<ICatalogIndexEntry> source)
        => source.Where(x => x.FilePath.EndsWith(".xml"));

    private async Task<Dictionary<string, IColour>> LoadColourMaps(string catalogFilePath)
    {
        var returnValue = new Dictionary<string, IColour>();

        var colourFileEntries = await catalogFileReader.ParseIndexes(catalogFilePath, Constants.CatalogFile.FileName.ColourXml, true);
        colourFileEntries = GetXmlFilesOnly(colourFileEntries);

        var colourFiles = catalogFileReader.ReadTextFiles(colourFileEntries);
        await foreach (var colourFile in colourFiles)
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(colourFile);

            var colourMap = await resourceFileReader.ReadColourMap(reader);

            foreach (var kvp in colourMap)
            {
                returnValue[kvp.Key] = kvp.Value;
            }
        }

        return returnValue;
    }
    
    private async Task<Dictionary<string, IFaction>> LoadFactions(string catalogFilePath)
    {
        var returnValue = new Dictionary<string, IFaction>();

        var factionFileEntries = await catalogFileReader.ParseIndexes(catalogFilePath, Constants.CatalogFile.FileName.FactionsXml, true);
        factionFileEntries = GetXmlFilesOnly(factionFileEntries);

        var factionFiles = catalogFileReader.ReadTextFiles(factionFileEntries);
        await foreach (var factionFile in factionFiles)
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(factionFile);

            var factions = await resourceFileReader.ReadFactions(reader);

            foreach (var kvp in factions)
            {
                returnValue[kvp.Key] = kvp.Value;
            }
        }

        return returnValue;
    }

    private async Task<Dictionary<string, string>> LoadSectorNameMap(string catalogFilePath)
    {
        var returnValue = new Dictionary<string, string>();

        var mapDefinitionEntries = await catalogFileReader.ParseIndexes(catalogFilePath, Constants.CatalogFile.FileName.MapDefinitionXml, true);
        mapDefinitionEntries = GetXmlFilesOnly(mapDefinitionEntries);

        var mapDefinitions = catalogFileReader.ReadTextFiles(mapDefinitionEntries);
        await foreach (var mapDefinition in mapDefinitions)
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(mapDefinition);

            var mapData = await resourceFileReader.ReadMapDataset(reader);

            foreach (var kvp in mapData)
            {
                returnValue[kvp.Key] = kvp.Value;
            }
        }

        return returnValue;
    }

    private async Task<Dictionary<int, Dictionary<int, string>>> LoadTextResources(string catalogFilePath)
    {
        var languageFile = await catalogFileReader.ReadTextFile(
                                                                catalogFilePath,
                                                                Constants.CatalogFile.FileId.TextResources,
                                                                Constants.CatalogFile.FileName.EnglishTextResource);

        using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(languageFile);
        return await resourceFileReader.ReadTextResource(reader);
    }

    private async Task<Dictionary<string, IZoneOffset>> LoadZoneOffsets(string catalogFilePath)
    {
        var returnValue = new Dictionary<string, IZoneOffset>();

        var mapFileEntries = await catalogFileReader.ParseIndexes(catalogFilePath, "maps/", true);
        mapFileEntries = GetXmlFilesOnly(mapFileEntries);

        var mapFiles = catalogFileReader.ReadTextFiles(mapFileEntries);
        await foreach (var mapFile in mapFiles)
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReaderFromXmlString(mapFile);

            var mapData = await resourceFileReader.ReadZoneOffset(reader);

            foreach (var kvp in mapData)
            {
                returnValue[kvp.Key] = kvp.Value;
            }
        }

        return returnValue;
    }

    private static string RecursiveMapName(string identity, Dictionary<int, Dictionary<int, string>> names)
    {
        if (string.IsNullOrWhiteSpace(identity))
        {
            return identity;
        }

        string returnValue = identity;
        returnValue = TextPlaceholderRegex().Replace(returnValue, match =>
        {
            string replacement;
            var segment = match.Value;

            var parts = segment.Split(',');
            if (parts.Length != 2)
            {
                return segment;
            }

            if (!int.TryParse(parts[0][1..], out int pageId) ||
                !int.TryParse(parts[1][..^1], out int entryId))
            {
                return segment;
            }

            if (names.TryGetValue(pageId, out var entryDict))
            {
                if (entryDict.TryGetValue(entryId, out var value))
                {
                    replacement = RecursiveMapName(value, names);
                }
                else
                {
                    replacement = $"MISSING_ENTRY_({pageId},{entryId})";
                }
            }
            else
            {
                replacement = $"MISSING_PAGE_{pageId}";
            }

            return replacement;
        });

        returnValue = TextParenthesesRegex().Replace(returnValue, "$1");

        return returnValue;
    }

    private static void RecursiveMapNames<T>(Dictionary<T, string> target, Dictionary<int, Dictionary<int, string>> names)
        where T : notnull
    {
        var keys = target.Keys.ToArray();
        foreach (var key in keys)
        {
            target[key] = RecursiveMapName(target[key], names);
        }
    }

    async Task<IResourceData> IResourceDataLoader.LoadFromCatalog(string catalogFilePath)
    {
        var results = await LoadTextResources(catalogFilePath);

        var landmarks = GetMappedTextDictionary(results, Constants.ResourceFile.PageId.Landmarks);
        var stationNames = GetMappedTextDictionary(results, Constants.ResourceFile.PageId.StationNames);
        var sectorNames = GetMappedTextDictionary(results, Constants.ResourceFile.PageId.SectorNames);

        var sectorNameMacros = await LoadSectorNameMap(catalogFilePath);
        RecursiveMapNames(sectorNameMacros, results);

        var zoneOffsets = await LoadZoneOffsets(catalogFilePath);

        var factions = await LoadFactions(catalogFilePath);
        foreach (var kvp in factions)
        {
            //kvp.Value.Name = RecursiveMapName(kvp.Value.NameResource, results);
        }

        var colourMaps = await LoadColourMaps(catalogFilePath);

        return new ResourceData(results)
        {
            ColourMap = colourMaps,
            Factions = factions,
            Landmarks = landmarks,
            SectorMacros = sectorNameMacros,
            SectorNames = sectorNames,
            StationNames = stationNames,
            ZoneOffsets = zoneOffsets
        };
    }
}
