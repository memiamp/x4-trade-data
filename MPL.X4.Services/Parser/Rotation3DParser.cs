using Microsoft.Extensions.Logging;

namespace MPL.X4.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IRotation3D"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class Rotation3DParser(
                                ILogger<Rotation3DParser> logger)
    : DataParserBase<IRotation3D>(logger)
{
    private protected override Task<IRotation3D> OnParse(IXmlReaderWrapper reader)
    {
        reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Pitch, out double? pitch);
        reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Roll, out double? roll);
        reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Yaw, out double? yaw);

        var returnValue = new Rotation3D
        {
            Pitch = pitch ?? 0,
            Roll = roll ?? 0,
            Yaw = yaw ?? 0
        };

        return Task.FromResult<IRotation3D>(returnValue);
    }
}
