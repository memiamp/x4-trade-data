using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IHighwayData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class HighwayDataParser(
                                 IDataParser dataParser,
                                 ILogger<HighwayDataParser> logger)
    : DataParserBase<IHighwayData>(dataParser, logger)
{
    private protected override async Task<IHighwayData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.HighwayId, out string? id))
        {
            logger.LogWarning("Could not parse highway");
            throw new ArgumentException("Could not parse highway", nameof(reader));
        }

        var isKnown = GetIsKnownToPlayer(reader);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro);

        var (ships, transform) = await ParseElements(reader);

        return new HighwayData
        {
            Code = code,
            Id = id,
            IsKnown = isKnown,
            Macro = macro ?? string.Empty,
            Ships = ships,
            Transform = transform
        };
    }

    private async Task<(IEnumerable<IShipData>, ITransform3D)> ParseElements(IXmlReaderWrapper reader)
    {
        List<IShipData> ships = [];
        ITransform3D transform = ITransform3D.GetDefault();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Offset, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                transform = await DataParser.Parse<ITransform3D>(subtree);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element, 3) &&
                     reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, x => x.StartsWith(Constants.XmlDataFile.AttributeValue.Class.Ship)))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<IShipData>(subtree);

                ships.AddRange(data);
            }
        }

        return (ships, transform);
    }
}
