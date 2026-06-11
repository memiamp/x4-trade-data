using Microsoft.Extensions.Logging;
using MPL.X4.Services.Xml;

namespace MPL.X4.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IRotation3D"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class Rotation3DParser(
                                IDataParser dataParser,
                                ILogger<Rotation3DParser> logger)
    : DataParserBase<IRotation3D>(dataParser, logger)
{
    private static Task<IRotation3D> ReturnResultInternal(double? pitch, double? roll, double? yaw)
    {
        var returnValue = new Rotation3D
        {
            Pitch = pitch ?? 0,
            Roll = roll ?? 0,
            Yaw = yaw ?? 0
        };

        return Task.FromResult<IRotation3D>(returnValue);
    }

    private protected override Task<IRotation3D> OnParse(IXDocumentWrapper document)
    {
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Pitch, out double? pitch);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Roll, out double? roll);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Yaw, out double? yaw);

        return ReturnResultInternal(pitch, roll, yaw);
    }

    private protected override Task<IRotation3D> OnParse(IXmlReaderWrapper reader)
    {
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Pitch, out double? pitch);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Roll, out double? roll);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Yaw, out double? yaw);

        return ReturnResultInternal(pitch, roll, yaw);
    }
}
