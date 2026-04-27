using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="IShip"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="positionParser">An <see cref="IDataParser{ISectorPosition}"/> that is the position parser to use.</param>
internal class ShipParser(
                          ILogger<ShipParser> logger,
                          IDataParser<ISectorPosition> positionParser)
    : PositionalDataParserBase<IShip>(logger, positionParser)
{
    private protected override async Task<IShip> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        SectorPosition position = new(positionOffset);
        Ship? returnValue;

        if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, out string? shipClass) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Owner, out string? owner) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out string? macro))
        {
            var isKnown = GetIsKnownToPlayer(reader);

            returnValue = new Ship
            {
                Class = shipClass,
                Code = code,
                Id = id,
                IsKnown = isKnown,
                Macro = macro,
                Owner = owner,
                Position = position
            };
        }
        else
        {
            logger.LogWarning("Could not load ship");
            throw new ArgumentException("Could not load ship", nameof(reader));
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
