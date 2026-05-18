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

        if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Connections, XmlNodeType.Element, 0))
        {
            using var subtree = await reader.ReadSubtree();

            var data = await ParseWeaponModification(subtree);

            returnValue.AddRange(data);
        }
        else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Modification, XmlNodeType.Element, 0))
        {
            using var subtree = await reader.ReadSubtree();

            var data = await ParseModifications(subtree);

            returnValue.AddRange(data);
        }
        else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Shields, XmlNodeType.Element, 0))
        {
            using var subtree = await reader.ReadSubtree();

            var data = await ParseShieldModification(subtree);

            returnValue.AddRange(data);
        }

        return returnValue;
    }

    private static async Task<IEnumerable<IModificationData>> ParseModifications(IXmlReaderWrapper reader)
    {
        var returnValue = new List<IModificationData>();

        while (await reader.ReadAsync())
        {
            if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
            {
                continue;
            }

            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Engine, XmlNodeType.Element, 1))
            {
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.BoostAcceleration, out double? boostAcceleration);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.BoostDuration, out double? boostDuration);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.BoostThrust, out double? boostThrust);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.ForwardThrust, out double? forwardThrust);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.RotationThrust, out double? rotationThrust);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.StrafeAcceleration, out double? strafeAcceleration);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.StrafeThrust, out double? strafeThrust);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.TravelAttackTime, out double? travelAttackTime);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.TravelChargeTime, out double? travelChargeTime);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.TravelStartThrust, out double? travelStartThrust);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.TravelThrust, out double? travelThrust);

                returnValue.Add(new EngineModificationData
                {
                    BoostAcceleration = boostAcceleration ?? 0,
                    BoostDuration = boostDuration ?? 0,
                    BoostThrust = boostThrust ?? 0,
                    ForwardThrust = forwardThrust ?? 0,
                    RotationThrust = rotationThrust ?? 0,
                    StrafeAcceleration = strafeAcceleration ?? 0,
                    StrafeThrust = strafeThrust ?? 0,
                    TravelAttackTime = travelAttackTime ?? 0,
                    TravelChargeTime = travelChargeTime ?? 0,
                    TravelStartThrust = travelStartThrust ?? 0,
                    TravelThrust = travelThrust ?? 0,
                    Ware = ware
                });
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Paint, XmlNodeType.Element, 1))
            {
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Generated, out int? generated);

                returnValue.Add(new PaintModificationData
                {
                    Generated = generated == 1,
                    Ware = ware
                });
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Ship, XmlNodeType.Element, 1))
            {
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.CountermeasureCapacity, out int? countermeasureCapacity);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.DeployableCapacity, out int? deployableCapacity);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Drag, out double? drag);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Mass, out double? mass);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.MaximumHull, out double? maximumHull);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.MissileCapacity, out int? missileCapacity);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.RadarCloak, out double? radarCloak);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.RadarRange, out double? radarRange);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.RegionDamage, out double? regionDamage);
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.UnitCapacity, out double? unitCapacity);

                returnValue.Add(new ShipModificationData
                {
                    CountermeasureCapacity = countermeasureCapacity ?? 0,
                    DeployableCapacity = deployableCapacity ?? 0,
                    Drag = drag ?? 0,
                    Mass = mass ?? 0,
                    MaximumHull= maximumHull ?? 0,
                    MissileCapacity = missileCapacity ?? 0,
                    RadarCloak = radarCloak ?? 0,
                    RadarRange = radarRange ?? 0,
                    RegionDamage = regionDamage ?? 0,
                    UnitCapacity = unitCapacity ?? 0,
                    Ware = ware
                });
            }
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
