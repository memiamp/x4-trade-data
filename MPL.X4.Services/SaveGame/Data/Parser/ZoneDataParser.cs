using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.DataParser;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

public interface IZoneDataParser : IDataParser<IZoneData>
{
    /// <summary>
    /// Gets or sets the zone offsets.
    /// </summary>
    Dictionary<string, IOffsetData> ZoneOffsets { get; set; }
}
/// <summary>
/// A class that implements a data parser for a <see cref="IZoneData"/>.
/// </summary>
/// <param name="gateParser">An <see cref="IDataParser{IGateData}"/> that is the gate parser to use.</param>
/// <param name="lockboxParser">An <see cref="IDataParser{ILockboxData}"/> that is the lockbox parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="positionParser">An <see cref="IDataParser{IPosition3D}"/> that is the position parser to use.</param>
/// <param name="shipParser">An <see cref="IDataParser{IShipData}"/> that is the ship parser to use.</param>
/// <param name="stationParser">An <see cref="IDataParser{IStationData}"/> that is the station parser to use.</param>
internal class ZoneDataParser(
                              IDataParser<IGateData> gateParser,
                              IDataParser<ILockboxData> lockboxParser,
                              ILogger<ZoneDataParser> logger,
                              IDataParser<IPosition3D> positionParser,
                              IDataParser<IShipData> shipParser,
                              IDataParser<IStationData> stationParser)
    : PositionalDataParserBase<IZoneData>(logger, positionParser),
      IZoneDataParser
{
    private Dictionary<string, IOffsetData> _zoneOffsets = [];

    Dictionary<string, IOffsetData> IZoneDataParser.ZoneOffsets 
    {
        get => _zoneOffsets; 
        set => _zoneOffsets = value; 
    }

    private protected override async Task<IZoneData> OnParse(IXmlReaderWrapper reader)
    //private protected override async Task<IZone> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        List<IGateData> gates = [];
        List<ILockboxData> lockboxes = [];
        //SectorPosition? offset = new(positionOffset);
        ZoneData? returnValue;
        List<IShipData> ships = [];
        List<IStationData> stations = [];
        string? macro = string.Empty;
        if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id))
        {
            var isKnown = GetIsKnownToPlayer(reader);

            if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out macro))
            {
                if (_zoneOffsets.TryGetValue(macro, out var zoneOffset))
                {
                    //UpdatePosition(zoneOffset.Position, offset);
                }
            }

            returnValue = new ZoneData
            {
                Code = code,
                Gates = gates,
                Id = id,
                IsKnown = isKnown,
                Lockboxes = lockboxes,
                Position = IPosition3D.GetDefault(),
                //Position = offset,
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
                //await UpdatePositionFromOffset(reader, offset);
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Gate))
            {
                using var gateSubtree = await reader.ReadSubtree();

                var gate = await gateParser.Parse(gateSubtree);
                //var gate = await gateParser.Parse(gateSubtree, offset);

                gates.Add(gate);
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Lockbox))
            {
                using var lockboxSubtree = await reader.ReadSubtree();

                var lockbox = await lockboxParser.Parse(lockboxSubtree);
                //var lockbox = await lockboxParser.Parse(lockboxSubtree, offset);

                lockboxes.Add(lockbox);
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x.StartsWith(Constants.SaveGameFile.AttributeValue.Class.Ship), out string? _))
            {
                using var shipSubtree = await reader.ReadSubtree();

                var ship = await shipParser.Parse(shipSubtree);
                //var ship = await shipParser.Parse(shipSubtree, offset);

                ships.AddRange(ship);
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Station))
            {
                using var stationSubtree = await reader.ReadSubtree();

                var station = await stationParser.Parse(stationSubtree);
                //var station = await stationParser.Parse(stationSubtree, offset);

                stations.Add(station);
            }
        }

        return returnValue;
    }
}
