using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IUniverseData"/>.
/// </summary>
/// <param name="sectorParser">An <see cref="IDataParser{ISector}"/> that is the sector parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class UniverseDataParser(
                                  IDataParser dataParser,
                                  ILogger<UniverseDataParser> logger)
    : DataParserBase<IUniverseData>(dataParser, logger)
{
    private protected override async Task<IUniverseData> OnParse(IXmlReaderWrapper reader)
    {
        var sectors = new List<ISectorData>();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element, 7) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, x => x == Constants.XmlDataFile.AttributeValue.Class.Sector))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<ISectorData>(subtree);

                sectors.Add(data);
            }
        }

        return new UniverseData
        {
            Sectors = sectors
        };
    }
}
