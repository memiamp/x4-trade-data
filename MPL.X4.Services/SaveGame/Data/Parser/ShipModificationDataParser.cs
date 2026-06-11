using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IShipModificationData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ShipModificationDataParser(
                                          IDataParser dataParser,
                                          ILogger<ShipModificationDataParser> logger)
    : DocumentDataParserBase<IShipModificationData>(dataParser, logger)
{
    private protected override Task<IShipModificationData> OnParse(IXDocumentWrapper document)
    {
        if (!document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
        {
            logger.LogWarning("Could not parse ship modification");
            throw new ArgumentException("Could not parse ship modification", nameof(document));
        }

        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.CountermeasureCapacity, out int? countermeasureCapacity);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.DeployableCapacity, out int? deployableCapacity);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Drag, out double? drag);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Mass, out double? mass);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.MaximumHull, out double? maximumHull);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.MissileCapacity, out int? missileCapacity);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.RadarCloak, out double? radarCloak);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.RadarRange, out double? radarRange);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.RegionDamage, out double? regionDamage);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.UnitCapacity, out double? unitCapacity);

        var returnValue = new ShipModificationData
        {
            CountermeasureCapacity = countermeasureCapacity ?? 0,
            DeployableCapacity = deployableCapacity ?? 0,
            Drag = drag ?? 0,
            Mass = mass ?? 0,
            MaximumHull = maximumHull ?? 0,
            MissileCapacity = missileCapacity ?? 0,
            RadarCloak = radarCloak ?? 0,
            RadarRange = radarRange ?? 0,
            RegionDamage = regionDamage ?? 0,
            UnitCapacity = unitCapacity ?? 0,
            Ware = ware
        };

        return Task.FromResult<IShipModificationData>(returnValue);
    }
}
