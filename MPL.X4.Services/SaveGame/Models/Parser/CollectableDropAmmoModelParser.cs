using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="ICollectableDropModelList"/> from an <see cref="ICollectableAmmoData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class CollectableDropAmmoModelParser(
                                              ILogger<CollectableDropAmmoModelParser> logger,
                                              ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<ICollectableAmmoData, ICollectableDropModelList>(logger)
{
    private protected override ICollectableDropModelList OnParse(ICollectableAmmoData source)
    {
        var returnValue = new CollectableDropModelList();

        var isWreck = source.State == Constants.XmlDataFile.AttributeValue.State.Wreck;
        var transform = source.Transform.Add(parsingScope.CurrentOffset);

        foreach (var item in source.Items)
        {
            var drop = ParseItem(item, source.Macro, source.IsKnown, isWreck, transform);
            returnValue.Add(drop);
        }

        return returnValue;
    }

    private ICollectableDropModel ParseItem(IAmmunitionItemData source, string id, bool isKnown, bool isWreck, ITransform3D transform)
    {
        if (!parsingScope.GameResources.Wares.TryGetValueByComponentReference(source.Macro, out var wareModel))
        {
            Logger.LogWarning("Could not parse ammunition item data as ware macro {WareMacro} is missing", source.Macro);
            throw new InvalidOperationException("Unable to parse ammunition item data due to missing ware macro");
        }

        var amount = source.Amount == 0 && isWreck == false ? 1 : source.Amount;

        return new CollectableDropModel()
        {
            Amount = amount,
            Id = id,
            IsKnown = isKnown,
            IsWreck = isWreck,
            Name = wareModel.Name,
            Transform = transform,
            Type = DropType.Ammo,
            WareType = wareModel.Type
        };
    }
}
