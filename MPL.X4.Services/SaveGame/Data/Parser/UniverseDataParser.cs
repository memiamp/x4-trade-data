using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IUniverseData"/>.
/// </summary>
/// <param name="sectorParser">An <see cref="IDataParser{ISector}"/> that is the sector parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class UniverseDataParser(
                                  IDataParser<ISectorData> sectorParser,
                                  ILogger<UniverseDataParser> logger)
    : DataParserBase<IUniverseData>(logger)
{
    private protected override async Task<IUniverseData> OnParse(IXmlReaderWrapper reader)
    {
        var sectors = new List<ISectorData>();

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

        return new UniverseData
        {
            Sectors = sectors
        };
    }
}
