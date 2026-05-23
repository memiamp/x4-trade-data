using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="ICollectableDropModelList"/> from an <see cref="ICollectableWareData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class CollectableDropWareModelParser(
                                              ILogger<CollectableDropWareModelParser> logger,
                                              ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<ICollectableWareData, ICollectableDropModelList>(logger)
{
    private protected override ICollectableDropModelList OnParse(ICollectableWareData source)
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

    private ICollectableDropModel ParseItem(IWareItemData source, string id, bool isKnown, bool isWreck, ITransform3D transform)
    {
        if (!parsingScope.GameResources.Wares.TryGetValue(source.Ware, out var wareModel))
        {
            Logger.LogWarning("Could not parse ware item data as ware {Ware} is missing", source.Ware);
            throw new InvalidOperationException("Unable to parse ware item data due to missing ware");
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
            Type = DropType.Ware,
            WareType = wareModel.Type
        };
    }
}
