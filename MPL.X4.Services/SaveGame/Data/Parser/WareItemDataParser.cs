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
    : DocumentDataParserBase<IWareItemData>(dataParser, logger)
{
    private protected override Task<IWareItemData> OnParse(IXDocumentWrapper document)
    {
        IWareItemData returnValue;
      
        if (document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
        {
            document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Amount, out int? amount);
            document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Buy, out int? buy);
            document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Price, out int? price);
            document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Sell, out int? sell);

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
            throw new ArgumentException("Could not load ware item", nameof(document));
        }

        return Task.FromResult(returnValue);
    }
}
