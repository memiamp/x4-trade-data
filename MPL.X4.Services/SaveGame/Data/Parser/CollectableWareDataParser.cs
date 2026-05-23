using Microsoft.Extensions.Logging;
using MPL.X4.Parser;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ICollectableWareData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class CollectableWareDataParser(
                                         IDataParser dataParser,
                                         ILogger<CollectableWareDataParser> logger)
    : CollectableDropDataParser<ICollectableWareData, IWareItemData, IEnumerable<IWareItemData>>(dataParser, logger)
{

    private protected override ICollectableWareData CreateResult(bool isKnown, IEnumerable<IWareItemData> items, string macro, string? state, ITransform3D transform)
        => new CollectableWareData
        {
            IsKnown = isKnown,
            Items = items,
            Macro = macro,
            State = state,
            Transform = transform
        };

    private protected override IEnumerable<IWareItemData> GetParserResult(IEnumerable<IWareItemData> data)
        => data;

    private protected override string DropNodeName => Constants.XmlDataFile.ElementName.Wares;
}
