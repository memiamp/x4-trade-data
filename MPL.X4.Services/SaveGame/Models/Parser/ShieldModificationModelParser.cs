using Microsoft.Extensions.Logging;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IShieldModificationModel"/> from an <see cref="IShieldModificationData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class ShieldModificationModelParser(
                                             ILogger<ShieldModificationModelParser> logger,
                                             ISaveGameModelParsingScope parsingScope)
    : ModificationModelParserBase<IShieldModificationData, IShieldModificationModel>(logger, parsingScope)
{
    private protected override IShieldModificationModel OnParse(IShieldModificationData source)
    {
        ParseNameAndQuality(source.Ware, out var name, out var quality);

        return new ShieldModificationModel
        {
            Capacity = source.Capacity,
            Id = source.Ware,
            Name = name,
            Quality = quality,
            RechargeDelay = source.RechargeDelay,
            RechargeRate = source.RechargeRate
        };
    }
}
