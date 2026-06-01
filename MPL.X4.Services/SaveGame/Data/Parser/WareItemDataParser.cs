using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IWareItemData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class WareItemDataParser(
                                  IDataParser dataParser,
                                  ILogger<WareItemDataParser> logger)
    : DataParserBase<IWareItemData>(dataParser, logger)
{
    private protected override Task<IWareItemData> OnParse(IXmlReaderWrapper reader)
    {
        IWareItemData returnValue;

        if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Ware, XmlNodeType.Element, 0) &&
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
        {
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Amount, out int? amount);
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Buy, out int? buy);
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Price, out int? price);
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Sell, out int? sell);

            returnValue = new WareItemData
            {
                Amount = amount ?? 0,
                Buy = buy ?? 0,
                Price = price ?? 0,
                Sell = sell ?? 0,
                Ware = ware
            };
        }
        else
        {
            logger.LogWarning("Could not load ware item");
            throw new ArgumentException("Could not load ware item", nameof(reader));
        }

        return Task.FromResult(returnValue);
    }
}
