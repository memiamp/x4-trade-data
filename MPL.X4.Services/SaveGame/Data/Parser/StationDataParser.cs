using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IStationData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class StationDataParser(
                                 IDataParser dataParser,
                                 ILogger<StationDataParser> logger)
    : DocumentDataParserBase<IStationData>(dataParser, logger)
{
    private protected override async Task<IStationData> OnParse(IXDocumentWrapper document)
    {
        if (!document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro) ||
            !document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Owner, out string? owner) ||
            !document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.StationId, out string? id))
        {
            logger.LogWarning("Could not parse station");
            throw new ArgumentException("Could not parse station", nameof(document));
        }

        var isKnown = GetIsKnownToPlayer(document);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.State, out string? state);

        ParseNames(document, out var baseNameResource, out var name, out var nameIndex, out var nameResource);

        var (buildModuleId, buildModuleConnectionId) = ParseBuildingModule(document);
        var cargo = await ParseCargo(document);
        var defenceModuleCount = ParseDefenceModules(document);
        var productions = ParseProductions(document);
        var ships = await ParseShips(document);
        var trades = await ParseTrades(document);
        var transform = await ParseTransform(document);

        return new StationData
        {
            BaseNameResource = baseNameResource,
            BuildingModuleConnectionId = buildModuleConnectionId,
            BuildingModuleId = buildModuleId,
            Cargo = cargo,
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
            Ships = ships,
            State = state,
            Trades = trades,
            Transform = transform
        };
    }

    private static (string?, string?) ParseBuildingModule(IXDocumentWrapper document)
    {
        document.TryGetAttribute(Constants.XmlDataFile.XPath.Station.BuildingModuleElement, Constants.XmlDataFile.AttributeName.BuildingModuleId, out string? buildingModuleId);
        document.TryGetAttribute(Constants.XmlDataFile.XPath.Station.BuildingModuleConnectedElement, Constants.XmlDataFile.AttributeName.Connection, out string? buildModuleConnectionId);

        return (buildingModuleId, buildModuleConnectionId);
    }

    private async Task<ICargoData> ParseCargo(IXDocumentWrapper document)
    {
        var cargoItems = new List<IWareItemData>();

        foreach (var item in document.SelectElementsAsDocument(Constants.XmlDataFile.XPath.Station.CargoElement))
        {
            var data = await DataParser.Parse<ICargoData>(item);
            cargoItems.AddRange(data.Items);
        }

        // Merge duplicated ware items
        var returnValue = cargoItems
                                    .GroupBy(x => x.Ware)
                                    .Select(x => new { x.Key, Amount = x.Sum(y => y.Amount) })
                                    .Select(x => new WareItemData
                                    {
                                        Amount = x.Amount,
                                        Buy = 0,
                                        Price = 0,
                                        Sell = 0,
                                        Ware = x.Key
                                    })
                                    .ToList();

        return new CargoData
        {
            Items = returnValue
        };
    }

    private static int ParseDefenceModules(IXDocumentWrapper document)
        => document.SelectElementsAsDocument(Constants.XmlDataFile.XPath.Station.DefenceModuleElement)?.Count() ?? 0;

    private static void ParseNames(IXDocumentWrapper document, out TextResourceReference? baseNameResource, out string? name, out int nameIndex, out TextResourceReference? nameResource)
    {
        name = null;
        nameResource = null;

        if (!document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.BaseName, out string? value) ||
            !TextResourceReference.TryParse(value, out baseNameResource))
        {
            baseNameResource = null;
        }

        if (document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Name, out string? rawName))
        {
            if (!TextResourceReference.TryParse(rawName, out nameResource))
            {
                name = rawName;
            }
        }

        if (document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.NameIndex, out int? nameIndexValue))
        {
            nameIndex = nameIndexValue.Value;
        }
        else
        {
            nameIndex = 0;
        }
    }

    private static IEnumerable<string> ParseProductions(IXDocumentWrapper document)
    {
        List<string> returnValue = [];

        // Single queued production types
        foreach (var item in document.SelectElementsAsDocument(Constants.XmlDataFile.XPath.Station.ProductionElement))
        {
            if (item.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
            {
                returnValue.Add(ware);
            }
        }

        // Multiple queued production types (recyled)
        foreach (var item in document.SelectElementsAsDocument(Constants.XmlDataFile.XPath.Station.ProductionItemElement))
        {
            if (item.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
            {
                returnValue.Add(ware);
            }
            else
            {
                Console.WriteLine("YHMM");
            }
        }

        return returnValue;
    }

    private async Task<IEnumerable<IShipData>> ParseShips(IXDocumentWrapper document)
    {
        List<IShipData> returnValue = [];

        // Get ships inside docking components
        foreach (var item in document.SelectElementsAsDocument(Constants.XmlDataFile.XPath.Docks.ShipsInDockElement))
        {
            var data = await DataParser.Parse<IShipData>(item);
            returnValue.Add(data);
        }

        // Get ships inside direct docking bays
        foreach (var item in document.SelectElementsAsDocument(Constants.XmlDataFile.XPath.Docks.ShipsInDockingBayElement))
        {
            var data = await DataParser.Parse<IShipData>(item);
            returnValue.Add(data);
        }

        return returnValue;
    }

    private async Task<IEnumerable<ITradeData>> ParseTrades(IXDocumentWrapper document)
    {
        List<ITradeData> returnValue = [];

        foreach (var item in document.SelectElementsAsDocument(Constants.XmlDataFile.XPath.Station.TradeElement))
        {
            var data = await DataParser.Parse<ITradeData>(item);
            returnValue.Add(data);
        }

        return returnValue;
    }

    private async Task<ITransform3D> ParseTransform(IXDocumentWrapper document)
    {
        var returnValue = await ParseElementOptional<ITransform3D>(document, Constants.XmlDataFile.XPath.Transform.OffsetElement);

        return returnValue ?? ITransform3D.GetDefault();
    }

    /*
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

        var (buildModuleId, buildModuleConnectionId, defenceModuleCount, productions, ships, trades, transform) = await ParseElements(reader);

        return new StationData
        {
            BaseNameResource = baseNameResource,
            BuildingModuleConnectionId = buildModuleConnectionId,
            BuildingModuleId = buildModuleId,
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
            Ships = ships,
            State = state,
            Trades = trades,
            Transform = transform
        };
    }

    private static async Task<BuildProcessorElements> ParseBuildingModule(IXmlReaderWrapper reader)
    {
        string? buildModuleConnectionId = null;

        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.BuildingModuleId, out string? buildModuleId);
        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Connected, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Connection, out buildModuleConnectionId))
            {
                break;
            }
        }

        return (buildModuleId, buildModuleConnectionId);
    }

    private async Task<(BuildProcessorElements, StationModuleElements)> ParseConnections(IXmlReaderWrapper reader)
    {
        string? buildModuleConnectionId = null;
        string? buildModuleId = null;
        int defenceModuleCount = 0;
        List<string> productions = [];
        List<IShipData> ships = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Connection, XmlNodeType.Element, 1))
            {
                if (reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Connection, x => x == Constants.XmlDataFile.AttributeValue.Connection.BuildingModule))
                {
                    using var subtree = await reader.ReadSubtree();

                    (buildModuleId, buildModuleConnectionId) = await ParseBuildingModule(subtree);
                }
                else
                {
                    using var subtree = await reader.ReadSubtree();

                    var (newDefenceModuleCount, newProductions, newShips) = await ParseModules(subtree);

                    defenceModuleCount += newDefenceModuleCount;
                    productions.AddRange(newProductions);
                    ships.AddRange(newShips);
                }
            }
        }

        return ((buildModuleId, buildModuleConnectionId), (defenceModuleCount, productions, ships));
    }

    private async Task<IEnumerable<IShipData>> ParseDocks(IXmlReaderWrapper reader)
    {
        List<IShipData> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Connection, XmlNodeType.Element, 5) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Connection, x => x == Constants.XmlDataFile.AttributeValue.Connection.Dock))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await ParseShips(subtree);

                returnValue.AddRange(data);
            }
        }

        return returnValue;
    }

    private async Task<StationElements> ParseElements(IXmlReaderWrapper reader)
    {
        string? buildModuleConnectionId = null;
        string? buildModuleId = null;
        int defenceModuleCount = 0;
        IEnumerable<string> productions = [];
        List<IShipData> ships = [];
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

                ((buildModuleId, buildModuleConnectionId), (defenceModuleCount, productions, var newShips)) = await ParseConnections(subtree);

                ships.AddRange(newShips);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Trade, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await ParseTrades(subtree);
                trades.AddRange(data);
            }
        }

        return (buildModuleId, buildModuleConnectionId, defenceModuleCount, productions, ships, trades, transform);
    }

    private async Task<StationModuleElements> ParseModules(IXmlReaderWrapper reader)
    {
        int defenceModuleCount = 0;
        List<string> productions = [];
        List<IShipData> ships = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, out string? moduleClass))
            {
                if (moduleClass == Constants.XmlDataFile.AttributeValue.Class.Production)
                {
                    using var subtree = await reader.ReadSubtree();

                    var data = await ParseProduction(subtree);
                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        productions.Add(data);
                    }
                }
                else if (moduleClass == Constants.XmlDataFile.AttributeValue.Class.DefenceModule)
                {
                    defenceModuleCount++;
                }
                else if (Constants.XmlDataFile.AttributeValue.Class.ClassesWithDocks.Contains(moduleClass))
                {
                    using var subtree = await reader.ReadSubtree();

                    // Look for ships in docking bays
                    var data = await ParseDocks(subtree);

                    ships.AddRange(data);
                }
            }
        }

        return (defenceModuleCount, productions, ships);
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

    private async Task<IEnumerable<IShipData>> ParseShips(IXmlReaderWrapper reader)
    {
        List<IShipData> returnValue = [];
        
        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, x => x.StartsWith(Constants.XmlDataFile.AttributeValue.Class.Ship)))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<IShipData>(subtree);

                returnValue.Add(data);
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
                using var subtree = await reader.ReadSubtree();

                returnValue = await ParseTrade(subtree);

                break;
            }
        }

        return returnValue;
    }*/
}
