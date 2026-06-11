using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ICargoData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class CargoDataParser(
                               IDataParser dataParser,
                               ILogger<CargoDataParser> logger)
    : DocumentDataParserBase<ICargoData>(dataParser, logger)
{
    private protected override async Task<ICargoData> OnParse(IXDocumentWrapper document)
    {
        var cargoItems = new List<IWareItemData>();

        foreach (var item in document.SelectElementsAsDocument(Constants.XmlDataFile.XPath.Cargo.WareElement))
        {
            var data = await DataParser.Parse<IWareItemData>(item);
            cargoItems.Add(data);
        }

        return new CargoData
        {
            Items = cargoItems
        };
    }
}
