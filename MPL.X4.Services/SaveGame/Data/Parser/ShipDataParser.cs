using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IShipData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ShipDataParser(
                              IDataParser dataParser,
                              ILogger<ShipDataParser> logger)
    : DocumentDataParserBase<IShipData>(dataParser, logger)
{
    private protected override async Task<IShipData> OnParse(IXDocumentWrapper document)
    {
        if (!document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Class, out string? shipClass) ||
            !document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Owner, out string? owner) ||
            !document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro) ||
            !document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.ShipId, out string? id))
        {
            logger.LogWarning("Could not load ship");
            throw new ArgumentException("Could not load ship", nameof(document));
        }

        var isKnown = GetIsKnownToPlayer(document);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Name, out string? name);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.State, out string? state);

        var cargo = await ParseCargo(document);
        var (engineModification, paintModification, shieldModification, shipModification) = await ParseModifications(document);
        var ships = await ParseShips(document);
        var transform = await ParseTransform(document);
        var weaponModifications = await ParseWeaponModifications(document);

        return new ShipData
        {
            Cargo = cargo,
            Class = shipClass,
            Code = code,
            EngineModification = engineModification,
            Id = id,
            IsKnown = isKnown,
            Macro = macro,
            Name = name,
            Owner = owner,
            PaintModification = paintModification,
            ShieldModification = shieldModification,
            ShipModification = shipModification,
            Ships = ships,
            Transform = transform,
            State = state,
            WeaponModifications = weaponModifications
        };
    }

    private async Task<ICargoData> ParseCargo(IXDocumentWrapper document)
    {
        var cargoItems = new List<IWareItemData>();

        foreach (var item in document.SelectElementsAsDocument(Constants.XmlDataFile.XPath.Ship.CargoElement))
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

    private async Task<(IEngineModificationData?, IPaintModificationData?, IShieldModificationData?, IShipModificationData?)> ParseModifications(IXDocumentWrapper document)
    {
        var engineModification = await ParseElementOptional<IEngineModificationData>(document, Constants.XmlDataFile.XPath.Ship.EngineModificationElement);
        var paintModification = await ParseElementOptional<IPaintModificationData>(document, Constants.XmlDataFile.XPath.Ship.PaintModificationElement);
        var shieldModification = await ParseElementOptional<IShieldModificationData>(document, Constants.XmlDataFile.XPath.Ship.ShieldModificationElement);
        var shipModification = await ParseElementOptional<IShipModificationData>(document, Constants.XmlDataFile.XPath.Ship.ShipModificationElement);

        return (engineModification, paintModification, shieldModification, shipModification);
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

    private async Task<ITransform3D> ParseTransform(IXDocumentWrapper document)
    {
        var returnValue = await ParseElementOptional<ITransform3D>(document, Constants.XmlDataFile.XPath.Transform.OffsetElement);

        return returnValue ?? ITransform3D.GetDefault();
    }

    private async Task<IEnumerable<IWeaponModificationData>> ParseWeaponModifications(IXDocumentWrapper document)
    {
        List<IWeaponModificationData> returnValue = [];

        foreach (var item in document.SelectElementsAsDocument(Constants.XmlDataFile.XPath.Ship.WeaponModificationElement))
        {
            var data = await DataParser.Parse<IWeaponModificationData>(item);
            returnValue.Add(data);
        }

        return returnValue;
    }
}
