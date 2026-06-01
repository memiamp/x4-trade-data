using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ISectorData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class SectorDataParser(
                                IDataParser dataParser,
                                ILogger<SectorDataParser> logger)
    : DataParserBase<ISectorData>(dataParser, logger)
{
    private protected override async Task<ISectorData> OnParse(IXmlReaderWrapper reader)
    {
        List<IZoneData> zones = [];

        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Owner, out string? owner) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.SectorId, out string? id))
        {
            Logger.LogWarning("Could not load sector");
            throw new ArgumentException("Could not load sector", nameof(reader));
        }

        var isKnown = GetIsKnownToPlayer(reader);

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element, 3) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, x => x == Constants.XmlDataFile.AttributeValue.Class.Zone))
            {
                using var subtree = await reader.ReadSubtree();

                var zone = await DataParser.Parse<IZoneData>(subtree);

                zones.Add(zone);
            }
        }

        return new SectorData
        {
            Code = code,
            Id = id,
            IsKnown = isKnown,
            Macro = macro,
            Owner = owner,
            Zones = zones
        };
    }
}
