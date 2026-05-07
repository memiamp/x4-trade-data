using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.DataParser;
using MPL.X4.Parser;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IGateData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="positionParser">An <see cref="IDataParser{IPosition3D}"/> that is the position parser to use.</param>
internal class GateDataParser(
                              ILogger<GateDataParser> logger,
                              IDataParser<IPosition3D> positionParser)
    : PositionalDataParserBase<IGateData>(logger, positionParser)
{
    private protected override async Task<IGateData> OnParse(IXmlReaderWrapper reader)
    //private protected override async Task<IGate> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        var position = new Position3D();
        //var position = new SectorPosition(positionOffset);
        GateData? returnValue;

        if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out string? type))
        {
            var isKnown = GetIsKnownToPlayer(reader);

            returnValue = new GateData
            {
                Code = code,
                Id = id,
                IsKnown = isKnown,
                Position = position
            };
        }
        else
        {
            Logger.LogWarning("Could not load gate");
            throw new ArgumentException("Could not load gate", nameof(reader));
        }

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Position, XmlNodeType.Element, 2))
            {
                await UpdatePosition(reader, position);
                break;
            }
        }

        return returnValue;
    }
}
