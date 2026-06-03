using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IEconomyLogData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class EconomyLogDataParser(
                                    IDataParser dataParser,
                                    ILogger<EconomyLogDataParser> logger)
    : DataParserBase<IEconomyLogData>(dataParser, logger)
{
    private protected override async Task<IEconomyLogData> OnParse(IXmlReaderWrapper reader)
    {
        ITradeLogData? tradeLog = null;

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Entries, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.LogEntriesType, x => x == Constants.XmlDataFile.AttributeValue.LogEntriesType.Trade))
            {
                using var subtree = await reader.ReadSubtree();

                tradeLog = await DataParser.Parse<ITradeLogData>(subtree);

                break;
            }
        }

        if (tradeLog is null)
        {
            logger.LogWarning("Could not load trade logs");
            throw new ArgumentException("Could not load trade logs", nameof(reader));
        }

        return new EconomyLogData
        {
            TradeLog = tradeLog
        };
    }
}
