using Microsoft.Extensions.Logging;
using MPL.X4.Services.Xml;

namespace MPL.X4.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IPosition3D"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class Position3DParser(
                                IDataParser dataParser, 
                                ILogger<Position3DParser> logger)
    : DataParserBase<IPosition3D>(dataParser, logger)
{
    private static Task<IPosition3D> ReturnResultInternal(double? x, double? y, double? z)
    {
        var returnValue = new Position3D
        {
            X = x ?? 0,
            Y = y ?? 0,
            Z = z ?? 0
        };

        return Task.FromResult<IPosition3D>(returnValue);
    }

    private protected override Task<IPosition3D> OnParse(IXDocumentWrapper document)
    {
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.X, out double? x);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Y, out double? y);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Z, out double? z);

        return ReturnResultInternal(x, y, z);
    }
    private protected override Task<IPosition3D> OnParse(IXmlReaderWrapper reader)
    {
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.X, out double? x);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Y, out double? y);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Z, out double? z);

        return ReturnResultInternal(x, y, z);
    }
}
