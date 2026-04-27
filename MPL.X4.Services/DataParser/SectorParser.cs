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
                            IDataParser<IZone> zoneParser)
    : DataParserBase<ISector>(logger)
{
    private const string ClusterClusterText = "cluster_";
    private const string ClusterSectorText = "_sector";

    private int GetSectorNameIdFromMacro(string source)
    {
        if (source.Length > 9)
        {
            var clusterEndPos = source.IndexOf('_', ClusterClusterText.Length + 1);
            var sectorStartPos = source.IndexOf(ClusterSectorText);
            var sectorEndPos = source.IndexOf('_', sectorStartPos + 1);

            if (clusterEndPos >= 0 &&
                sectorStartPos >= 0 &&
                sectorEndPos >= 0)
            {
                var clusterText = source[ClusterClusterText.Length..source.IndexOf('_', ClusterClusterText.Length)];
                var sectorText = source[(sectorStartPos + ClusterSectorText.Length)..sectorEndPos];

                var sectorIdText = $"{clusterText}{sectorText}";
                if (int.TryParse(sectorIdText, out var returnValue))
                {
                    return returnValue;
                }
            }
        }

        Logger.LogWarning("Cannot parse sector identifier from {SourceValue}", source);
        throw new ArgumentException($"Cannot parse sector identifier from {source}", nameof(source));
    }

    private protected override async Task<ISector> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        ISector? returnValue = null;
        List<ILockbox> lockboxes = [];
        List<IShip> ships = [];
        List<IStation> stations = [];

        if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out string? macro) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Owner, out string? owner))
        {
            var isKnown = GetIsKnownToPlayer(reader);
            var nameId = GetSectorNameIdFromMacro(macro);

            returnValue = new Sector
            {
                Code = code,
                Id = id,
                IsKnown = isKnown,
                Lockboxes = lockboxes,
                NameId = nameId,
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
                lockboxes.AddRange(zone.Lockboxes);
                ships.AddRange(zone.Ships);
                stations.AddRange(zone.Stations);
            }
        }

        return returnValue;
    }
}
