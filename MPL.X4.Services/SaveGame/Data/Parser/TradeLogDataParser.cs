using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ITradeLogData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class TradeLogDataParser(
                                  IDataParser dataParser,
                                  ILogger<TradeLogDataParser> logger)
    : DataParserBase<ITradeLogData>(dataParser, logger)
{
    private protected override async Task<ITradeLogData> OnParse(IXmlReaderWrapper reader)
    {
        var returnValue = new TradeLogData();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Log, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.LogType, x => x == Constants.XmlDataFile.AttributeValue.LogType.Trade))
            {
                using var subtree = await reader.ReadSubtree();

                var item = await DataParser.Parse<ITradeLogEntryData>(subtree);

                returnValue.Add(item);
            }
        }

        return returnValue;
    }
}
