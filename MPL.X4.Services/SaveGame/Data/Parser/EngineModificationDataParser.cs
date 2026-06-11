using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IEngineModificationData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class EngineModificationDataParser(
                                            IDataParser dataParser,
                                            ILogger<EngineModificationDataParser> logger)
    : DocumentDataParserBase<IEngineModificationData>(dataParser, logger)
{
    private protected override Task<IEngineModificationData> OnParse(IXDocumentWrapper document)
    {
        if (!document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
        {
            logger.LogWarning("Could not parse engine modification");
            throw new ArgumentException("Could not parse engine modification", nameof(document));
        }

        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.BoostAcceleration, out double? boostAcceleration);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.BoostDuration, out double? boostDuration);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.BoostThrust, out double? boostThrust);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.ForwardThrust, out double? forwardThrust);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.RotationThrust, out double? rotationThrust);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.StrafeAcceleration, out double? strafeAcceleration);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.StrafeThrust, out double? strafeThrust);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.TravelAttackTime, out double? travelAttackTime);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.TravelChargeTime, out double? travelChargeTime);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.TravelStartThrust, out double? travelStartThrust);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.TravelThrust, out double? travelThrust);

        var returnValue = new EngineModificationData
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
        };

        return Task.FromResult<IEngineModificationData>(returnValue);
    }
}
