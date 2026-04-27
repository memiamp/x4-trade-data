using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="IZone"/>.
/// </summary>
/// <param name="lockboxParser">An <see cref="IDataParser{ILockbox}"/> that is the lockbox parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="positionParser">An <see cref="IDataParser{ISectorPosition}"/> that is the position parser to use.</param>
/// <param name="shipParser">An <see cref="IDataParser{IShip}"/> that is the ship parser to use.</param>
/// <param name="stationParser">An <see cref="IDataParser{IStation}"/> that is the station parser to use.</param>
internal class ZoneParser(
                          IDataParser<ILockbox> lockboxParser,
                          ILogger<ZoneParser> logger,
                          IDataParser<ISectorPosition> positionParser,
                          IDataParser<IShip> shipParser,
                          IDataParser<IStation> stationParser)
    : PositionalDataParserBase<IZone>(logger, positionParser)
{
    private protected override async Task<IZone> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        List<ILockbox> lockboxes = [];
        ISectorPosition? offset = ISectorPosition.GetDefault();
        Zone? returnValue;
        List<IShip> ships = [];
        List<IStation> stations = [];

        if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id))
        {
            var isKnown = GetIsKnownToPlayer(reader);

            returnValue = new Zone
            {
                Code = code,
                Id = id,
                IsKnown = isKnown,
                Lockboxes = lockboxes,
                Position = positionOffset,
                Ships = ships,
                Stations = stations
            };
        }
        else
        {
            logger.LogWarning("Could not load zone");
            throw new ArgumentException("Could not load zone", nameof(reader));
        }

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Position, XmlNodeType.Element, 2))
            {
                offset = await PositionParser.Parse(reader);
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Station))
            {
                using var stationSubtree = await reader.ReadSubtree();

                var station = await stationParser.Parse(stationSubtree, offset);

                stations.Add(station);
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Lockbox))
            {
                using var lockboxSubtree = await reader.ReadSubtree();

                var lockbox = await lockboxParser.Parse(lockboxSubtree, offset);

                lockboxes.Add(lockbox);
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x.StartsWith(Constants.SaveGameFile.AttributeValue.Class.Ship), out string? _))
            {
                using var shipSubtree = await reader.ReadSubtree();

                var ship = await shipParser.Parse(shipSubtree, offset);

                ships.AddRange(ship);
            }
        }

        return returnValue;
    }
}
