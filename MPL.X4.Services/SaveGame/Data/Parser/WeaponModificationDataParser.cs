using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IWeaponModificationData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class WeaponModificationDataParser(
                                            IDataParser dataParser,
                                            ILogger<WeaponModificationDataParser> logger)
    : DocumentDataParserBase<IWeaponModificationData>(dataParser, logger)
{
    private protected override Task<IWeaponModificationData> OnParse(IXDocumentWrapper document)
    {
        if (!document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
        {
            logger.LogWarning("Could not parse waeapon modification");
            throw new ArgumentException("Could not parse waeapon modification", nameof(document));
        }

        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.ChargeTime, out double? chargeTime);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Cooling, out double? cooling);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Damage, out double? damage);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Lifetime, out double? lifetime);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Mining, out double? mining);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Reload, out double? reload);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.RotationSpeed, out double? rotationSpeed);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Sticktime, out double? stickTime);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.SurfaceElement, out double? surfaceElement);

        var returnValue = new WeaponModificationData
        {
            ChargeTime = chargeTime ?? 0,
            Cooling = cooling ?? 0,
            Damage = damage ?? 0,
            Lifetime = lifetime ?? 0,
            Mining = mining ?? 0,
            Reload = reload ?? 0,
            RotationSpeed = rotationSpeed ?? 0,
            StickTime = stickTime ?? 0,
            SurfaceElement = surfaceElement ?? 0,
            Ware = ware,
        };

        return Task.FromResult<IWeaponModificationData>(returnValue);
    }
}
