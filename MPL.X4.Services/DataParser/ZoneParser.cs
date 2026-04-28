using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="IZone"/>.
/// </summary>
/// <param name="gateParser">An <see cref="IDataParser{IGate}"/> that is the gate parser to use.</param>
/// <param name="lockboxParser">An <see cref="IDataParser{ILockbox}"/> that is the lockbox parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="positionParser">An <see cref="IDataParser{ISectorPosition}"/> that is the position parser to use.</param>
/// <param name="shipParser">An <see cref="IDataParser{IShip}"/> that is the ship parser to use.</param>
/// <param name="stationParser">An <see cref="IDataParser{IStation}"/> that is the station parser to use.</param>
internal class ZoneParser(
                          IDataParser<IGate> gateParser,
                          IDataParser<ILockbox> lockboxParser,
                          ILogger<ZoneParser> logger,
                          IDataParser<ISectorPosition> positionParser,
                          IDataParser<IShip> shipParser,
                          IDataParser<IStation> stationParser)
    : PositionalDataParserBase<IZone>(logger, positionParser)
{
    private readonly Dictionary<string, IZoneOffset> _zoneOffsets = new()
    {
        { "zone004_cluster_601_sector001_macro", new ZoneOffset()
            {
                MacroName = "zone004_cluster_601_sector001_macro",
                OffsetPosition = new SectorPosition() { X = -127517.8, Y = 0, Z = 34255.37 },
                OffsetRotation = ISectorRotation.GetDefault()
            }
        },
        { "zone001_cluster_601_sector001_macro", new ZoneOffset()
            {
                MacroName = "zone001_cluster_601_sector001_macro",
                OffsetPosition = new SectorPosition() { X = 85.55412, Y = 0, Z = 115545.4 },
                OffsetRotation = ISectorRotation.GetDefault()
            }
        },
        { "zone002_cluster_601_sector001_macro", new ZoneOffset()
            {
                MacroName = "",
                OffsetPosition = new SectorPosition() { X = 137328.5, Y = 0, Z = 138576.5 },
                OffsetRotation = ISectorRotation.GetDefault()
            }
        },
        { "zone003_cluster_601_sector001_macro", new ZoneOffset()
            {
                MacroName = "zone003_cluster_601_sector001_macro",
                OffsetPosition = new SectorPosition() { X = 105554.4, Y = 0, Z = -127903.8 },
                OffsetRotation = ISectorRotation.GetDefault()
            }
        },
        { "zone005_cluster_601_sector001_macro", new ZoneOffset()
            {
                MacroName = "zone005_cluster_601_sector001_macro",
                OffsetPosition = new SectorPosition() { X = -129500.2, Y = 0, Z = -126969.5 },
                OffsetRotation = ISectorRotation.GetDefault()
            }
        },
        { "zone001_cluster_14_sector001_macro", new ZoneOffset()
            {
                MacroName = "zone001_cluster_14_sector001_macro",
                OffsetPosition = new SectorPosition() { X = 89982.4921875, Y = 0, Z =-100098.1640625 },
                OffsetRotation = ISectorRotation.GetDefault()
            }
        },
        { "zone002_cluster_14_sector001_macro", new ZoneOffset()
            {
                MacroName = "zone002_cluster_14_sector001_macroXX",
                OffsetPosition = new SectorPosition() { X = -57062.328125, Y = 0, Z = 61362.80859375 },
                OffsetRotation = ISectorRotation.GetDefault()
            }
        },
        { "zone003_cluster_14_sector001_macro", new ZoneOffset()
            {
                MacroName = "zone003_cluster_14_sector001_macro",
                OffsetPosition = new SectorPosition() { X = 45571.9921875, Y = 0, Z = 108137.7109375 },
                OffsetRotation = ISectorRotation.GetDefault()
            }
        },
        { "zone004_cluster_14_sector001_macro", new ZoneOffset()
            {
                MacroName = "zone004_cluster_14_sector001_macro",
                OffsetPosition = new SectorPosition() { X = 50742.0078125, Y = 0, Z = 5897.970703125 },
                OffsetRotation = ISectorRotation.GetDefault()
            }
        }
    };

    private protected override async Task<IZone> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        List<IGate> gates = [];
        List<ILockbox> lockboxes = [];
        SectorPosition? offset = new(positionOffset);
        Zone? returnValue;
        List<IShip> ships = [];
        List<IStation> stations = [];
        string? macro = string.Empty;
        if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id))
        {
            var isKnown = GetIsKnownToPlayer(reader);

            if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out macro))
            {
                if (_zoneOffsets.TryGetValue(macro, out var zoneOffset))
                {
                    Console.WriteLine($"{macro} Zone offset before: {offset}");
                    UpdatePosition(zoneOffset.OffsetPosition, offset);
                    Console.WriteLine($"{macro} Zone offset after : {offset}");
                }
            }

            returnValue = new Zone
            {
                Code = code,
                Gates = gates,
                Id = id,
                IsKnown = isKnown,
                Lockboxes = lockboxes,
                Position = offset,
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
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Offset, XmlNodeType.Element, 1))
            {
                if (macro is not null && _zoneOffsets.TryGetValue(macro, out _))
                    Console.WriteLine($"{macro} position offset before: {offset}");
                await UpdatePositionFromOffset(reader, offset);
                if (macro is not null && _zoneOffsets.TryGetValue(macro, out _))
                    Console.WriteLine($"{macro} position offset after : {offset}");
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Gate))
            {
                using var gateSubtree = await reader.ReadSubtree();

                var gate = await gateParser.Parse(gateSubtree, offset);

                gates.Add(gate);
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
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Station))
            {
                using var stationSubtree = await reader.ReadSubtree();

                var station = await stationParser.Parse(stationSubtree, offset);

                stations.Add(station);
            }
        }

        return returnValue;
    }
}
