using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Data;
using MPL.X4.Services.Data;
using MPL.X4.Services.DataParser;
using MPL.X4.Services.Models;

namespace MPL.X4.Services;

/// <summary>
/// A class that implements a resource file reader.
/// </summary>
/// <param name="factionsParser">An <see cref="IDataParser{IFactionsData}"/> that is the factions parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="xmlReaderFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XmlReader factory to use.</param>
/// <param name="zoneOffsetParser">An <see cref="IDataParser{IZoneOffset}"/> that is the zone offset parser to use.</param>
internal class ResourceFileReader(
                                  IDataParser<IFactionsData> factionsParser,
                                  ILogger<ResourceFileReader> logger,
                                  IXmlReaderWrapperFactory xmlReaderFactory,
                                  IDataParser<IZoneOffset> zoneOffsetParser)
    : IResourceFileReader
{
    private static async Task<Dictionary<string, IColour>> ReadColours(IXmlReaderWrapper reader)
    {
        Dictionary<string, IColour> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Colour, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.ColourId, out string? id))
            {
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.ColourAlpha, out int? alpha);
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.ColourBlue, out int? blue);
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.ColourGlow, out int? glow);
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.ColourGreen, out int? green);
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.ColourRed, out int? red);

                var colour = new Colour
                {
                    Alpha = alpha ?? 0,
                    Blue = blue ?? 0,
                    Glow = glow ?? 0,
                    Green = green ?? 0,
                    Id = id,
                    Red = red ?? 0
                };

                returnValue.Add(id, colour);
            }
        }

        return returnValue;
    }

    //private async Task<IFaction> ReadFaction(IXmlReaderWrapper reader)
    //{
    //    Faction? returnValue;

    //    if (reader.TryGetAttribute(Constants.ResourceFile.AttributeName.FactionId, out string? id))
    //    {
    //        reader.TryGetAttribute(Constants.ResourceFile.AttributeName.Name, out string? name);
    //        reader.TryGetAttribute(Constants.ResourceFile.AttributeName.ShortName, out string? shortName);

    //        returnValue = new Faction
    //        {
    //            AcronymResource = shortName is not null ? new TextResourceReference(shortName) : null,
    //            Id = id,
    //            NameResource = name is not null ? new TextResourceReference(name) : null
    //        };
    //    }
    //    else
    //    {
    //        logger.LogWarning("Could not load faction");
    //        throw new ArgumentException("Could not load faction", nameof(reader));
    //    }

    //    while (await reader.ReadAsync())
    //    {
    //        if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Colour, XmlNodeType.Element, 1) &&
    //            reader.TryGetAttribute(Constants.ResourceFile.AttributeName.Ref, out string? colourReference))
    //        {
    //            returnValue.ColourRef = colourReference;
    //            break;
    //        }
    //    }

    //    return returnValue;
    //}

    private static async Task<string> ReadDatasetName(IXmlReaderWrapper reader)
    {
        var returnValue = string.Empty;

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Identification, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.Name, out string? name))
            {
                returnValue = name;
                break;
            }
        }

        return returnValue;
    }

    private static async Task<Dictionary<string, string>> ReadMappings(IXmlReaderWrapper reader)
    {
        Dictionary<string, string> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Mapping, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.MappingId, out string? id) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.Ref, out string? reference))
            {
                returnValue.Add(id, reference);
            }
        }

        return returnValue;
    }

    private static async Task<Dictionary<int, string>> ReadTextResourceInternal(IXmlReaderWrapper reader)
    {
        var returnValue = new Dictionary<int, string>();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.TextEntry, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.TextId, out int? textId))
            {
                using var subtree = await reader.ReadSubtree();

                string value = await subtree.ReadElementContentAsStringAsync();

                returnValue.Add(textId.Value, value);
            }
        }

        return returnValue;
    }

    private static async Task<Dictionary<int, Dictionary<int, string>>> ReadTextResourceInternal(IXmlReaderWrapper reader, int[]? pageIds)
    {
        Dictionary<int, Dictionary<int, string>> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Page, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.PageId, out int? pageId) &&
                (pageIds is null ||
                pageIds?.Contains(pageId.Value) == true))
            {
                using var pageSubtree = await reader.ReadSubtree();

                var pageResults = await ReadTextResourceInternal(pageSubtree);

                returnValue.Add(pageId.Value, pageResults);
            }
        }

        return returnValue;
    }

    async Task<Dictionary<string, IColour>> IResourceFileReader.ReadColourMap(IXmlReaderWrapper reader)
    {
        Dictionary<string, IColour> colours = [];
        Dictionary<string, string> mappings = [];
        Dictionary<string, IColour> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Colours, XmlNodeType.Element))
            {
                using var colourSubtree = await reader.ReadSubtree();

                colours = await ReadColours(colourSubtree);
            }
            else if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Mappings, XmlNodeType.Element))
            {
                using var mappingSubtree = await reader.ReadSubtree();

                mappings = await ReadMappings(mappingSubtree);
            }
        }

        if (colours.Count > 0 &&
            mappings.Count > 0)
        {
            returnValue = mappings
                                  .ToDictionary(
                                                kvp => kvp.Key,
                                                kvp => colours[kvp.Value]);
        }

        return returnValue;
    }

    async Task<IFactionsData> IResourceFileReader.ReadFactions(IXmlReaderWrapper reader)
    {
        FactionsData returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Factions, XmlNodeType.Element))
            {
                using var factionSubTree = await reader.ReadSubtree();

                var factions = await factionsParser.Parse(factionSubTree);

                returnValue.Merge(factions);
            }
        }

        return returnValue;
    }

    async Task<Dictionary<string, string>> IResourceFileReader.ReadMapDataset(IXmlReaderWrapper reader)
    {
        Dictionary<string, string> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Dataset, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.Macro, out string? macro))
            {
                using var datasetSubtree = await reader.ReadSubtree();

                macro = macro.ToLower();

                if (!returnValue.ContainsKey(macro))
                {
                    var name = await ReadDatasetName(datasetSubtree);
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        returnValue.Add(macro, name);
                    }
                }
            }
        }

        return returnValue;
    }
    
    Task<Dictionary<int, Dictionary<int, string>>> IResourceFileReader.ReadTextResource(IXmlReaderWrapper reader, params int[] pageIds)
        => ReadTextResourceInternal(reader, pageIds);

    Task<Dictionary<int, Dictionary<int, string>>> IResourceFileReader.ReadTextResource(IXmlReaderWrapper reader)
        => ReadTextResourceInternal(reader, null);

    async Task<Dictionary<int, string>> IResourceFileReader.ReadTextResource(string sourcePath, int pageId)
    {
        var results = await ((IResourceFileReader)this).ReadTextResource(sourcePath, [pageId]);
        return results[pageId];
    }

    async Task<Dictionary<int, Dictionary<int, string>>> IResourceFileReader.ReadTextResource(string sourcePath, params int[] pageIds)
    {
        logger.LogInformation("Reading resource pages from {SourcePath}", sourcePath);

        using var reader = xmlReaderFactory.CreateXmlReader(sourcePath);

        return await ReadTextResourceInternal(reader, pageIds);
    }

    async Task<Dictionary<string, IZoneOffset>> IResourceFileReader.ReadZoneOffset(IXmlReaderWrapper reader)
    {
        Dictionary<string, IZoneOffset> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Connection, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.Ref, x => x == Constants.ResourceFile.AttributeValue.Ref.Zones, out string? _))
            {
                using var zoneSubtree = await reader.ReadSubtree();

                var zoneOffset = await zoneOffsetParser.Parse(zoneSubtree);

                returnValue.TryAdd(zoneOffset.MacroName, zoneOffset);
            }
        }

        return returnValue;
    }
}
