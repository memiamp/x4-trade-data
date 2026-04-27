using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="ITrade"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class TradeParser(
                           ILogger<TradeParser> logger)
    : DataParserBase<ITrade>(logger)
{
    private protected override Task<ITrade> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        Trade? returnValue;

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

            returnValue = new Trade
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

        return Task.FromResult<ITrade>(returnValue);
    }
}
