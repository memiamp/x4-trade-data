using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="IUniverse"/>.
/// </summary>
/// <param name="sectorParser">An <see cref="IDataParser{ISector}"/> that is the sector parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class UniverseParser(
                              IDataParser<ISector> sectorParser,
                              ILogger<UniverseParser> logger)
    : DataParserBase<IUniverse>(logger)
{
    private protected override async Task<IUniverse> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        var sectors = new List<ISector>();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Sector))
            {
                using var sectorSubtree = await reader.ReadSubtree();

                var sector = await sectorParser.Parse(sectorSubtree);

                sectors.Add(sector);
            }
        }

        return new Universe
        {
            Sectors = sectors
        };
    }
}
