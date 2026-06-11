using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IBuildStorageData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class BuildStorageDataParser(
                                      IDataParser dataParser,
                                      ILogger<BuildStorageDataParser> logger)
    : DocumentDataParserBase<IBuildStorageData>(dataParser, logger)
{
    private protected override async Task<IBuildStorageData> OnParse(IXDocumentWrapper document)
    {
        if (!document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro) ||
            !document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Owner, out string? owner) ||
            !document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.BuildStorageId, out string? id))
        {
            logger.LogWarning("Could not parse build storage");
            throw new ArgumentException("Could not parse build storage", nameof(document));
        }

        var isKnown = GetIsKnownToPlayer(document);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.State, out string? state);

        var (buildAnchorId, buildAnchorConnectionId) = ParseBuildingModule(document);
        var cargo = await ParseCargo(document);
        var ships = await ParseShips(document);
        var trades = await ParseTrades(document);
        var transform = await ParseTransform(document);

        return new BuildStorageData
        {
            BuildAnchorConnectionId = buildAnchorConnectionId,
            BuildAnchorId = buildAnchorId,
            Cargo = cargo,
            Code = code,
            Id = id,
            IsKnown = isKnown,
            Macro = macro,
            Owner = owner,
            Ships = ships,
            State = state,
            Trades = trades,
            Transform = transform
        };
    }

    private static (string?, string?) ParseBuildingModule(IXDocumentWrapper document)
    {
        document.TryGetAttribute(Constants.XmlDataFile.XPath.BuildStorage.BuildAnchorConnection, Constants.XmlDataFile.AttributeName.BuildAnchorId, out string? buildAnchorId);
        document.TryGetAttribute(Constants.XmlDataFile.XPath.BuildStorage.BuildAnchorConnected, Constants.XmlDataFile.AttributeName.Connection, out string? buildAnchorConnectionId);

        return (buildAnchorId, buildAnchorConnectionId);
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
