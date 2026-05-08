using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ITradeData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class TradeDataParser(
                               ILogger<TradeDataParser> logger)
    : DataParserBase<ITradeData>(logger)
{
    private protected override Task<ITradeData> OnParse(IXmlReaderWrapper reader)
    {
        TradeData? returnValue;

        if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Trade, XmlNodeType.Element) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Price, out int? price) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Ware, out string? ware))
        {
            var amountToBuy = 0;
            var amountToSell = 0;

            if (!reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Amount, out int? amount) &&
                !reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Desired, out amount))
            {
                amount = 0;
            }

            if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Buyer, out string? _))
            {
                amountToBuy = amount.Value;
            }

            if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Seller, out string? _))
            {
                amountToSell = amount.Value;
            }

            returnValue = new TradeData
            {
                AmountToBuy = amountToBuy,
                AmountToSell = amountToSell,
                Id = id,
                Price = price.Value,
                Ware = ware
            };
        }
        else
        {
            logger.LogWarning("Could not load trade");
            throw new ArgumentException("Could not load trade", nameof(reader));
        }

        return Task.FromResult<ITradeData>(returnValue);
    }
}
