using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IWeaponModificationModel"/> from an <see cref="IWeaponModificationData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class WeaponModificationModelParser(
                                             ILogger<WeaponModificationModelParser> logger,
                                             ISaveGameModelParsingScope parsingScope)
    : ModificationModelParserBase<IWeaponModificationData, IWeaponModificationModel>(logger, parsingScope)
{
    private protected override IWeaponModificationModel OnParse(IWeaponModificationData source)
    {
        ParseNameAndQuality(source.Ware, out var name, out var quality);

        return new WeaponModificationModel
        {
            ChargeTime = source.ChargeTime,
            Cooling = source.Cooling,
            Damage = source.Damage,
            Id = source.Ware,
            Lifetime = source.Lifetime,
            Mining = source.Mining,
            Name = name,
            Quality = quality,
            Reload = source.Reload,
            RotationSpeed = source.RotationSpeed,
            StickTime = source.StickTime,
            SurfaceElement = source.SurfaceElement
        };
    }
}
