using Microsoft.Extensions.Logging;
using MPL.X4.Parser;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ICollectableAmmoData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class CollectableAmmoDataParser(
                                         IDataParser dataParser,
                                         ILogger<CollectableAmmoDataParser> logger)
    : CollectableDropDataParser<ICollectableAmmoData, IAmmunitionItemData, IAmmunitionItemData>(dataParser, logger)
{

    private protected override ICollectableAmmoData CreateResult(bool isKnown, IEnumerable<IAmmunitionItemData> items, string macro, string? state, ITransform3D transform)
        => new CollectableAmmoData
        {
            IsKnown = isKnown,
            Items = items,
            Macro = macro,
            State = state,
            Transform = transform
        };

    private protected override IEnumerable<IAmmunitionItemData> GetParserResult(IAmmunitionItemData data)
        => [data];

    private protected override string DropNodeName => Constants.XmlDataFile.ElementName.Ammunition;
}
