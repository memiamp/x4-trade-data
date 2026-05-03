using System.Diagnostics;
using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="IShip"/>.
/// </summary>
/// <param name="cargoParser">An <see cref="IDataParser{ICargo}"/> that is the cargo parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="positionParser">An <see cref="IDataParser{ISectorPosition}"/> that is the position parser to use.</param>
internal class ShipParser(
                          IDataParser<ICargo> cargoParser,
                          ILogger<ShipParser> logger,
                          IDataParser<ISectorPosition> positionParser)
    : PositionalDataParserBase<IShip>(logger, positionParser)
{
    private protected override async Task<IShip> OnParse(IXmlReaderWrapper reader)
    //private protected override async Task<IShip> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        var cargoItems = new List<ICargoItem>();
        SectorPosition position = new();
        //SectorPosition position = new(positionOffset);
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
                Cargo = new Cargo
                {
                    Items = cargoItems
                },
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
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Offset, XmlNodeType.Element, 1))
            {
                await UpdatePositionFromOffset(reader, position);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Cargo, XmlNodeType.Element))
            {
                using var cargoSubtree = await reader.ReadSubtree();

                var cargoes = await cargoParser.Parse(cargoSubtree);

                cargoItems.AddRange(cargoes.Items);
            }
        }

        return returnValue;
    }
}
