using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ITradeData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class TradeDataParser(
                               IDataParser dataParser,
                               ILogger<TradeDataParser> logger)
    : DocumentDataParserBase<ITradeData>(dataParser, logger)
{
    private protected override Task<ITradeData> OnParse(IXDocumentWrapper document)
    {
        var amountToBuy = 0;
        var amountToSell = 0;

        if (!document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Price, out int? price) ||
            !document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.TradeId, out string? id) ||
            !document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
        {
            logger.LogWarning("Could not parse trade");
            throw new ArgumentException("Could not parse trade", nameof(document));
        }

        if (!document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Amount, out int? amount) &&
            !document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Desired, out amount))
        {
            amount = 0;
        }

        if (document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Buyer, out string? _))
        {
            amountToBuy = amount.Value;
        }

        if (document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Seller, out string? _))
        {
            amountToSell = amount.Value;
        }

        var returnValue = new TradeData
        {
            AmountToBuy = amountToBuy,
            AmountToSell = amountToSell,
            Id = id,
            Price = price.Value,
            Ware = ware
        };

        return Task.FromResult<ITradeData>(returnValue);
    }
}
