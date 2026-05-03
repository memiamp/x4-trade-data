using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="ISector"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="zoneParser">An <see cref="IDataParser{IZone}"/> that is the zone parser to use.</param>
internal class SectorParser(
                            ILogger<SectorParser> logger,
                            IZoneParser zoneParser)
                            //IDataParser<IZone> zoneParser)
    : DataParserBase<ISector>(logger)
{
    private protected override async Task<ISector> OnParse(IXmlReaderWrapper reader)
    {
        List<IGate> gates = [];
        List<ILockbox> lockboxes = [];
        ISector? returnValue = null;
        List<IShip> ships = [];
        List<IStation> stations = [];

        if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out string? macro) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Owner, out string? owner))
        {
            var isKnown = GetIsKnownToPlayer(reader);

            returnValue = new Sector
            {
                Code = code,
                Gates = gates,
                Id = id,
                IsKnown = isKnown,
                Lockboxes = lockboxes,
                Macro = macro,
                Owner = owner,
                Ships = ships,
                Stations = stations
            };
        }
        else
        {
            Logger.LogWarning("Could not load sector");
            throw new ArgumentException("Could not load sector", nameof(reader));
        }

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Zone))
            {
                using var zoneSubtree = await reader.ReadSubtree();

                var zone = await zoneParser.Parse(zoneSubtree);
                gates.AddRange(zone.Gates);
                lockboxes.AddRange(zone.Lockboxes);
                ships.AddRange(zone.Ships);
                stations.AddRange(zone.Stations);
            }
        }

        return returnValue;
    }
}
