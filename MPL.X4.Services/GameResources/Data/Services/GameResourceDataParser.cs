using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Data.Services;

/// <summary>
/// A class that implements a game resource data parser.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class GameResourceDataParser(
                                      IDataParser dataParser,
                                      ILogger<GameResourceDataParser> logger)
    : IGameResourceDataParser
{
    private static async Task<string> ReadDataset(IXmlReaderWrapper reader)
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

    private static async Task<Dictionary<string, string>> ReadDefaultsInternal(IXmlReaderWrapper reader)
    {
        Dictionary<string, string> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Dataset, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro))
            {
                using var subtree = await reader.ReadSubtree();

                macro = macro.ToLower();
                var name = await ReadDataset(subtree);

                if (!string.IsNullOrWhiteSpace(name))
                {
                    returnValue[macro] = name;
                }
            }
        }

        return returnValue;
    }

    /*
     * 
     *   private static async Task<Dictionary<string, string>> ReadMappings(IXmlReaderWrapper reader)
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
     * (*/
    async Task<IColourResourceData> IGameResourceDataParser.ReadColourResources(IXmlReaderWrapper reader)
    {
        var colours = new ColourDataDictionary();
        var mappings = new MappingDataDictionary();

        logger.LogDebug("Parsing colour resource data");

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Colours, XmlNodeType.Element))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await dataParser.Parse<IColourDataDictionary>(subtree);

                colours.Merge(data);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Mappings, XmlNodeType.Element))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await dataParser.Parse<IMappingDataDictionary>(subtree);

                mappings.Merge(data);
            }
        }

        logger.LogDebug("{Count} colours parsed", colours.Count);
        logger.LogDebug("{Count} cplour mappings parsed", mappings.Count);

        return new ColourResourceData
        {
            Colours = colours,
            Mappings = mappings
        };
    }

    async Task<IFactionDataDictionary> IGameResourceDataParser.ReadFactions(IXmlReaderWrapper reader)
    {
        FactionDataDictionary returnValue = [];

        logger.LogDebug("Parsing faction data");

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Factions, XmlNodeType.Element))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await dataParser.Parse<IFactionDataDictionary>(subtree);

                returnValue.Merge(data);
            }
        }

        logger.LogDebug("{Count} factions parsed", returnValue.Count);

        return returnValue;
    }

    async Task<Dictionary<string, string>> IGameResourceDataParser.ReadSectorMacroMap(IXmlReaderWrapper reader)
    {
        Dictionary<string, string> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Defaults, XmlNodeType.Element, 0))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await ReadDefaultsInternal(subtree);

                foreach (var (key, value) in data)
                {
                    returnValue[key] = value;
                }
            }
        }

        return returnValue;
    }

    async Task<ITextResourcePageDictionary> IGameResourceDataParser.ReadTextResources(IXmlReaderWrapper reader)
    {
        TextResourcePageDictionary returnValue = [];

        logger.LogDebug("Parsing text resouce data");
     
        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Page, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await dataParser.Parse<ITextResourcePage>(subtree);

                returnValue.Add(data.Id, data);
            }
        }

        logger.LogDebug("{Count} pages parsed", returnValue.Count);

        return returnValue;
    }

    async Task<IZoneOffsetDataDictionary> IGameResourceDataParser.ReadZoneOffsets(IXmlReaderWrapper reader)
    {
        ZoneOffsetDataDictionary returnValue = [];

        logger.LogDebug("Parsing zone offset data");

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Macros, XmlNodeType.Element))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await dataParser.Parse<IZoneOffsetDataDictionary>(subtree);

                returnValue.Merge(data);
            }
        }

        logger.LogDebug("{Count} zone offsets parsed", returnValue.Count);

        return returnValue;
    }
}
