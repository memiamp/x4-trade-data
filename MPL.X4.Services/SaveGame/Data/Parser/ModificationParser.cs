using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IEnumerable{IModificationData}"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ModificationParser(
                                  IDataParser dataParser,
                                  ILogger<ModificationParser> logger)
    : DataParserBase<IEnumerable<IModificationData>>(dataParser, logger)
{
    private protected override async Task<IEnumerable<IModificationData>> OnParse(IXmlReaderWrapper reader)
    {
        var returnValue = new List<IModificationData>();

        if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Shields, XmlNodeType.Element, 0))
        {
            using var subtree = await reader.ReadSubtree();

            var data = await ParseShieldModification(subtree);

            returnValue.AddRange(data);
        }
        else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Connections, XmlNodeType.Element, 0))
        {
            using var subtree = await reader.ReadSubtree();

            var data = await ParseWeaponModification(subtree);

            returnValue.AddRange(data);
        }

        return returnValue;
    }

    private static async Task<IEnumerable<IModificationData>> ParseShieldModification(IXmlReaderWrapper reader)
    {
        var returnValue = new List<IModificationData>();
     
        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Modification, XmlNodeType.Element, 2) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
            {
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Capacity, out double? capacity);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.RechargeDelay, out double? rechargeDelay);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.RechargeRate, out double? rechargeRate);

                returnValue.Add(new ShieldModificationData
                {
                    Capacity = capacity ?? 0,
                    RechargeDelay = rechargeDelay ?? 0,
                    RechargeRate = rechargeRate ?? 0,
                    Ware = ware
                });
            }
        }

        return returnValue;
    }

    private static async Task<IEnumerable<IModificationData>> ParseWeaponModification(IXmlReaderWrapper reader)
    {
        var returnValue = new List<IModificationData>();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Modification, XmlNodeType.Element, 3) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
            {
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.ChargeTime, out double? chargeTime);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Cooling, out double? cooling);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Damage, out double? damage);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Lifetime, out double? lifetime);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Mining, out double? mining);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Reload, out double? reload);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.RotationSpeed, out double? rotationSpeed);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Sticktime, out double? stickTime);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.SurfaceElement, out double? surfaceElement);

                returnValue.Add(new WeaponModificationData
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
                });
            }
        }

        return returnValue;
    }

}
