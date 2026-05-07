using Microsoft.Extensions.Logging;

namespace MPL.X4.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IPosition3D"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class Position3DParser(
                                ILogger<Position3DParser> logger)
    : DataParserBase<IPosition3D>(logger)
{
    private protected override Task<IPosition3D> OnParse(IXmlReaderWrapper reader)
    {
        reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.X, out double? x);
        reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Y, out double? y);
        reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Z, out double? z);

        var returnValue = new Position3D
        {
            X = x ?? 0,
            Y = y ?? 0,
            Z = z ?? 0
        };

        return Task.FromResult<IPosition3D>(returnValue);
    }
}
