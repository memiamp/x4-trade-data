using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

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
        logger.LogDebug("{Count} colour mappings parsed", mappings.Count);

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

    async Task<IOffsetDataDictionary> IGameResourceDataParser.ReadOffsets(IXmlReaderWrapper reader)
    {
        OffsetDataDictionary returnValue = [];

        logger.LogDebug("Parsing offset data");

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Macros, XmlNodeType.Element))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await dataParser.Parse<IOffsetDataDictionary>(subtree);

                returnValue.Merge(data);
            }
        }

        logger.LogDebug("{Count} offsets parsed", returnValue.Count);

        return returnValue;
    }

    async Task<IMacroNameResourceDataDictionary> IGameResourceDataParser.ReadSectorNames(IXmlReaderWrapper reader)
    {
        IMacroNameResourceDataDictionary returnValue = new MacroNameResourceDataDictionary();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Defaults, XmlNodeType.Element, 0))
            {
                using var subtree = await reader.ReadSubtree();

                returnValue = await dataParser.Parse<ISectorNameResourceDataDictionary>(subtree);

                break;
            }
        }

        logger.LogDebug("{Count} sector names parsed", returnValue.Count);

        return returnValue;
    }

    async Task<IMacroNameResourceDataDictionary> IGameResourceDataParser.ReadShipModels(IXmlReaderWrapper reader)
    {
        IMacroNameResourceDataDictionary returnValue = new MacroNameResourceDataDictionary();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Macros, XmlNodeType.Element, 0))
            {
                using var subtree = await reader.ReadSubtree();

                returnValue = await dataParser.Parse<IShipModelResourceDataDictionary>(subtree);

                break;
            }
        }

        logger.LogDebug("{Count} ship models parsed", returnValue.Count);

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

    async Task<IMacroNameResourceDataDictionary> IGameResourceDataParser.ReadWareNames(IXmlReaderWrapper reader)
    {
        IMacroNameResourceDataDictionary returnValue = new MacroNameResourceDataDictionary();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Wares, XmlNodeType.Element, 0))
            {
                using var subtree = await reader.ReadSubtree();

                returnValue = await dataParser.Parse<IWareNameResourceDataDictionary>(subtree);

                break;
            }
        }

        logger.LogDebug("{Count} ware names parsed", returnValue.Count);

        return returnValue;
    }

}
