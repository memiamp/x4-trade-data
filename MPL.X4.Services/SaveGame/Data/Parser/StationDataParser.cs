using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

using StationElements = (
                         int DefenceModuleCount,
                         IEnumerable<string> Productions,
                         IEnumerable<ITradeData> Trades,
                         ITransform3D Transform);

using StationModuleElements = (
                               int DefenceModuleCount,
                               IEnumerable<string> Productions);

/// <summary>
/// A class that implements a data parser for a <see cref="IStationData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class StationDataParser(
                                 IDataParser dataParser,
                                 ILogger<StationDataParser> logger)
    : DataParserBase<IStationData>(dataParser, logger)
{
    private protected override async Task<IStationData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Owner, out string? owner) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.StationId, out string? id))
        {
            logger.LogWarning("Could not load station");
            throw new ArgumentException("Could not load station", nameof(reader));
        }

        var isKnown = GetIsKnownToPlayer(reader);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.State, out string? state);

        ParseNames(reader, out var baseNameResource, out var name, out var nameIndex, out var nameResource);

        var (defenceModuleCount, productions, trades, transform) = await ParseElements(reader);

        return new StationData
        {
            BaseNameResource = baseNameResource,
            Code = code,
            DefenceModuleCount = defenceModuleCount,
            Id = id,
            IsKnown = isKnown,
            Macro = macro,
            Name = name,
			NameIndex = nameIndex,
            NameResource = nameResource,
            Owner = owner,
            Productions = productions,
            State = state,
            Trades = trades,
            Transform = transform
        };
    }

    private static async Task<StationModuleElements> ParseConnections(IXmlReaderWrapper reader)
    {
        int defenceModuleCount = 0;
        List<string> productions = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Connection, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var (newDefenceModuleCount, newProductions) = await ParseModules(subtree);

                defenceModuleCount += newDefenceModuleCount;
                productions.AddRange(newProductions);
            }
        }

        return (defenceModuleCount, productions.Distinct());
    }

    private async Task<StationElements> ParseElements(IXmlReaderWrapper reader)
    {
        int defenceModuleCount = 0;
        IEnumerable<string> productions = [];
        List<ITradeData> trades = [];
        ITransform3D transform = ITransform3D.GetDefault();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Offset, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                transform = await DataParser.Parse<ITransform3D>(subtree);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Connections, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                (defenceModuleCount, productions) = await ParseConnections(subtree);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Trade, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await ParseTrades(subtree);
                trades.AddRange(data);
            }
        }

        return (defenceModuleCount, productions, trades, transform);
    }

    private static async Task<StationModuleElements> ParseModules(IXmlReaderWrapper reader)
    {
        int defenceModuleCount = 0;
        List<string> productions = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, out string? moduleClass))
            {
                if (moduleClass == Constants.XmlDataFile.AttributeValue.Class.Production)
                {
                    using var subtree = await reader.ReadSubtree();

                    var data = await ParseProduction(subtree);
                    if (!string.IsNullOrWhiteSpace(data) &&
                        !productions.Contains(data))
                    {
                        productions.Add(data);
                    }
                }
                else if (moduleClass == Constants.XmlDataFile.AttributeValue.Class.DefenceModule)
                {
                    defenceModuleCount++;
                }
            }
        }

        return (defenceModuleCount, productions.Distinct());
    }

    private static void ParseNames(IXmlReaderWrapper reader, out TextResourceReference? baseNameResource, out string? name, out int nameIndex, out TextResourceReference? nameResource)
    {
        name = null;
        nameResource = null;

        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.BaseName, out string? value) ||
            !TextResourceReference.TryParse(value, out baseNameResource))
        {
            baseNameResource = null;
        }

        if (reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Name, out string? rawName))
        {
            if (!TextResourceReference.TryParse(rawName, out nameResource))
            {
                name = rawName;
            }
        }

        if (reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.NameIndex, out int? nameIndexValue))
        {
            nameIndex = nameIndexValue.Value;
        }
        else
        {
            nameIndex = 0;
        }
    }

    private static async Task<string?> ParseProduction(IXmlReaderWrapper reader)
    {
		string? returnValue = null;

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Queue, XmlNodeType.Element, 2) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
            {
				returnValue = ware;
            }
        }

        return returnValue;
    }

    private async Task<IEnumerable<ITradeData>> ParseTrade(IXmlReaderWrapper reader)
    {
        List<ITradeData> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Trade, XmlNodeType.Element))
            {
                var data = await DataParser.Parse<ITradeData>(reader);

                returnValue.Add(data);
            }
        }

        return returnValue;
    }

    private async Task<IEnumerable<ITradeData>> ParseTrades(IXmlReaderWrapper reader)
    {
        IEnumerable<ITradeData> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Production, XmlNodeType.Element, 2))
            {
                returnValue = await ParseTrade(reader);
                break;
            }
        }

        return returnValue;
    }
}
