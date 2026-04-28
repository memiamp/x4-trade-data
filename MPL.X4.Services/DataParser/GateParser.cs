using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="IGate"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="positionParser">An <see cref="IDataParser{ISectorPosition}"/> that is the position parser to use.</param>
internal class GateParser(
                          ILogger<GateParser> logger,
                          IDataParser<ISectorPosition> positionParser)
    : PositionalDataParserBase<IGate>(logger, positionParser)
{
    private protected override async Task<IGate> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        var position = new SectorPosition(positionOffset);
        Gate? returnValue;

        if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out string? type))
        {
            var isKnown = GetIsKnownToPlayer(reader);

            returnValue = new Gate
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
