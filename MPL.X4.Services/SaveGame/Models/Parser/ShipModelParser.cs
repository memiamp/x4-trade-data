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
        { "ship_xl", ShipClass.ExtraLarge},
        { "ship_xs", ShipClass.ExtraSmall},
        { "ship_l", ShipClass.Large},
        { "ship_m", ShipClass.Medium},
        { "ship_s", ShipClass.Small}
    };

    private protected override IShipModel OnParse(IShipData source)
    {
        var cargo = modelParser.Parse<IEnumerable<IWareItemData>, ICargoItemModelList>(source.Cargo.Items);
        var model = ParseModel(source.Macro);
        var shipClass = ParseShipClass(source.Class);
        var transform = source.Transform.Add(parsingScope.CurrentOffset);

        //this is WHERE YOU ARE
        //source.Modifications
        return new ShipModel
        {
            Cargo = cargo,
            Class = shipClass,
            Code = source.Code,
            IsKnown = source.IsKnown,
            Id = source.Id,
            Model = model,
            Name = source.Name,
            Owner = source.Owner,
            Transform = transform,
        };
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
}
