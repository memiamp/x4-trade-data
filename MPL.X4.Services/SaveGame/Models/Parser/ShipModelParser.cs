using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IShipModel"/> from an <see cref="IShipData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class ShipModelParser(
                               ILogger<ShipModelParser> logger,
                               IModelParser modelParser,
                               ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<IShipData, IShipModel>(logger)
{
    private static readonly Dictionary<string, ShipClass> _typeMap = new()
    {
        { Constants.XmlDataFile.AttributeValue.Class.ShipExtraLarge, ShipClass.ExtraLarge},
        { Constants.XmlDataFile.AttributeValue.Class.ShipExtraSmall, ShipClass.ExtraSmall},
        { Constants.XmlDataFile.AttributeValue.Class.ShipLarge, ShipClass.Large},
        { Constants.XmlDataFile.AttributeValue.Class.ShipMedium, ShipClass.Medium},
        { Constants.XmlDataFile.AttributeValue.Class.ShipSmall, ShipClass.Small}
    };

    private protected override IShipModel OnParse(IShipData source)
    {
        var cargo = modelParser.Parse<IEnumerable<IWareItemData>, ICargoItemModelList>(source.Cargo.Items);
        var model = ParseModel(source.Macro);
        var owner = parsingScope.ParseFaction(source.Owner);
        var shipClass = ParseShipClass(source.Class);
        var ships = ParseShips(source.Ships);
        var transform = source.Transform.Add(parsingScope.CurrentOffset);

        ParseModifications(
                           source,
                           out var engineModification,
                           out var paintModification,
                           out var shieldModification,
                           out var shipModification,
                           out var weaponModifications);

        return new ShipModel
        {
            Cargo = cargo,
            Class = shipClass,
            Code = source.Code,
            EngineModification = engineModification,
            IsAbandoned = owner.IsOwnerless,
            IsKnown = source.IsKnown,
            Id = source.Id,
            IsWreck = source.State == Constants.XmlDataFile.AttributeValue.State.Wreck,
            Model = model,
            Name = source.Name,
            Owner = owner,
            PaintModification = paintModification,
            ShieldModification = shieldModification,
            ShipModification = shipModification,
            Ships = ships,
            Transform = transform,
            WeaponModifications = weaponModifications
        };
    }

    private void ParseModifications(
                                    IShipData source,
                                    out IEngineModificationModel? engineModification,
                                    out IPaintModificationModel? paintModification,
                                    out IShieldModificationModel? shieldModification,
                                    out IShipModificationModel? shipModification,
                                    out IEnumerable<IWeaponModificationModel> weaponModifications)
    {
        engineModification = null;
        paintModification = null;
        shieldModification = null;
        shipModification = null;
        weaponModifications = [];

        if (source.EngineModification is not null)
        {
            engineModification = modelParser.Parse<IEngineModificationData, IEngineModificationModel>(source.EngineModification);
        }

        if (source.PaintModification is not null)
        {
            paintModification = modelParser.Parse<IPaintModificationData, IPaintModificationModel>(source.PaintModification);
        }

        if (source.ShieldModification is not null)
        {
            shieldModification = modelParser.Parse<IShieldModificationData, IShieldModificationModel>(source.ShieldModification);
        }

        if (source.ShipModification is not null)
        {
            shipModification = modelParser.Parse<IShipModificationData, IShipModificationModel>(source.ShipModification);
        }

        if (source.WeaponModifications.Any())
        {
            var weapons = new List<IWeaponModificationModel>();

            foreach (var item in source.WeaponModifications)
            {
                var model = modelParser.Parse<IWeaponModificationData, IWeaponModificationModel>(item);
                weapons.Add(model);
            }

            weaponModifications = weapons;
        }
    }

    private string ParseModel(string macro)
    {
        var returnValue = string.Empty;
        if (parsingScope.GameResources.ShipModels.TryGetValue(macro, out var model))
        {
            returnValue = model.Name;
        }

        return returnValue;
    }

    private ShipClass ParseShipClass(string shipClass)
    {
        if (!_typeMap.TryGetValue(shipClass, out var returnValue))
        {
            Logger.LogWarning("Unable to map ship class {ShipClass}", shipClass);

            returnValue = ShipClass.Unknown;
        }

        return returnValue;
    }

    private IShipModelList ParseShips(IEnumerable<IShipData> source)
    {
        var returnValue = new ShipModelList();

        foreach (var item in source)
        {
            var model = modelParser.Parse<IShipData, IShipModel>(item);
            returnValue.Add(model);
        }

        return returnValue;
    }
}
