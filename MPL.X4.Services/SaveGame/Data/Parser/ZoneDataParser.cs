using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

using ZoneElements = (
                      IEnumerable<IGateData> Gates,
                      IEnumerable<ILockboxData> Lockboxes,
                      IEnumerable<IShipData> Ships,
                      IEnumerable<IStationData> Stations,
                      ITransform3D Transform);

/// <summary>
/// A class that implements a data parser for a <see cref="IZoneData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ZoneDataParser(
                              IDataParser dataParser,
                              ILogger<ZoneDataParser> logger)
    : DataParserBase<IZoneData>(dataParser, logger)
{
    private protected override async Task<IZoneData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.ZoneId, out string? id))
        {
            logger.LogWarning("Could not load zone");
            throw new ArgumentException("Could not load zone", nameof(reader));
        }

        var isKnown = GetIsKnownToPlayer(reader);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro);

        var (gates, lockboxes, ships, stations, transform) = await ParseElements(reader);

        return new ZoneData
        {
            Code = code,
            Gates = gates,
            Id = id,
            IsKnown = isKnown,
            Lockboxes = lockboxes,
            Macro = macro ?? string.Empty,
            Ships = ships,
            Stations = stations,
            Transform = transform
        };
    }

    private async Task<ZoneElements> ParseElements(IXmlReaderWrapper reader)
    {
        List<IGateData> gates = [];
        List<ILockboxData> lockboxes = [];
        List<IShipData> ships = [];
        List<IStationData> stations = [];
        ITransform3D transform = ITransform3D.GetDefault();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Offset, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                transform = await DataParser.Parse<ITransform3D>(subtree);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, x => x == Constants.XmlDataFile.AttributeValue.Class.Gate))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<IGateData>(subtree);

                gates.Add(data);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, x => x == Constants.XmlDataFile.AttributeValue.Class.Lockbox))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<ILockboxData>(subtree);

                lockboxes.Add(data);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element, 3) &&
                     reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, x => x.StartsWith(Constants.XmlDataFile.AttributeValue.Class.Ship)))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<IShipData>(subtree);

                ships.AddRange(data);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, x => x == Constants.XmlDataFile.AttributeValue.Class.Station))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<IStationData>(subtree);

                stations.Add(data);
            }
        }

        return (gates, lockboxes, ships, stations, transform);
    }
}
