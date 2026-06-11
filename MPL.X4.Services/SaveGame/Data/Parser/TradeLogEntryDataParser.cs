using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ITradeLogEntryData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class TradeLogEntryDataParser(
                                       IDataParser dataParser,
                                       ILogger<TradeLogEntryDataParser> logger)
    : DataParserBase<ITradeLogEntryData>(dataParser, logger)
{
    private protected override Task<ITradeLogEntryData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Buyer, out string? buyer) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Price, out int? price) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Seller, out string? seller) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Time, out double? time) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.TradeLogVolume, out int? volume) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
        {
            logger.LogWarning("Could not load trade log entry");
            throw new ArgumentException("Could not load trade log entry", nameof(reader));
        }

        var returnValue = new TradeLogEntryData
        {
            BuyerId = buyer,
            SellerId = seller,
            Time = time.Value,
            Price = price.Value,
            Volume = volume.Value,
            Ware = ware
        };

        return Task.FromResult<ITradeLogEntryData>(returnValue);
    }
}
