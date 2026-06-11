using Microsoft.Extensions.Logging;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IEngineModificationModel"/> from an <see cref="IEngineModificationData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class EngineModificationModelParser(
                                             ILogger<EngineModificationModelParser> logger,
                                             ISaveGameModelParsingScope parsingScope)
    : ModificationModelParserBase<IEngineModificationData, IEngineModificationModel>(logger, parsingScope)
{
    private protected override IEngineModificationModel OnParse(IEngineModificationData source)
    {
        ParseNameAndQuality(source.Ware, out var name, out var quality);

        return new EngineModificationModel
        {
            BoostAcceleration = source.BoostAcceleration,
            BoostDuration = source.BoostDuration,
            BoostThrust = source.BoostThrust,
            ForwardThrust = source.ForwardThrust,
            Id = source.Ware,
            Name = name,
            Quality = quality,
            RotationThrust = source.RotationThrust,
            StrafeAcceleration = source.StrafeAcceleration,
            StrafeThrust = source.StrafeThrust,
            TravelAttackTime = source.TravelAttackTime,
            TravelChargeTime = source.TravelChargeTime,
            TravelStartThrust = source.TravelStartThrust,
            TravelThrust = source.TravelThrust
        };
    }
}
