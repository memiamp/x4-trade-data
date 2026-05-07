using Microsoft.Extensions.Logging;

namespace MPL.X4.Services.DataParser;

internal class SectorRotationParser(
                                    ILogger<SectorRotationParser> logger)
    : DataParserBase<ISectorRotation>(logger)
{
    private protected override Task<ISectorRotation> OnParse(IXmlReaderWrapper reader)
    {
        reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Pitch, out double? pitch);
        reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Roll, out double? roll);
        reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Yaw, out double? yaw);

        var returnValue = new SectorRotation
        {
            Pitch = pitch ?? 0,
            Roll = roll ?? 0,
            Yaw = yaw ?? 0
        };

        return Task.FromResult<ISectorRotation>(returnValue);
    }
}
