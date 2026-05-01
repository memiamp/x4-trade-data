using Microsoft.Extensions.Logging;

namespace MPL.X4.Services.DataParser;

internal class SectorPositionParser(
                                    ILogger<SectorPositionParser> logger)
    : DataParserBase<ISectorPosition>(logger)
{
    private protected override Task<ISectorPosition> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.X, out double? x);
        reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Y, out double? y);
        reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Z, out double? z);

        var returnValue = new SectorPosition
        {
            X = (x ?? 0) + positionOffset.X,
            Y = (y ?? 0) + positionOffset.Y,
            Z = (z ?? 0) + positionOffset.Z
        };

        return Task.FromResult<ISectorPosition>(returnValue);
    }
}
