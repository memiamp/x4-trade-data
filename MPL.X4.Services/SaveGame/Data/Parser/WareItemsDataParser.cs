using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IEnumerable{IWareItemData}"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class WareItemsDataParser(
                                   IDataParser dataParser,
                                   ILogger<WareItemsDataParser> logger)
    : DataParserBase<IEnumerable<IWareItemData>>(dataParser, logger)
{
    private protected override async Task<IEnumerable<IWareItemData>> OnParse(IXmlReaderWrapper reader)
    {
        var returnValue = new List<IWareItemData>();

        if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Wares, XmlNodeType.Element, 0))
        {
            while (await reader.ReadAsync())
            {
                if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Ware, XmlNodeType.Element, 1))
                {
                    using var subtree = await reader.ReadSubtree();

                    var data = await DataParser.Parse<IWareItemData>(subtree);

                    returnValue.Add(data);
                }
            }
        }
        else
        {
            logger.LogWarning("Could not parse ware items");
            throw new ArgumentException("Could not parse ware items", nameof(reader));
        }

        return returnValue;
    }
}
