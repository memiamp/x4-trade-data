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
}
