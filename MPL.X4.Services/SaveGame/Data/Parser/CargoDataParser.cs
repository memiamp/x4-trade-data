using System.Xml;
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
    : DataParserBase<ICargoData>(dataParser, logger)
{
    private protected override async Task<ICargoData> OnParse(IXmlReaderWrapper reader)
    {
        var cargoItems = new List<ICargoItemData>();
        var returnValue = new CargoData
        {
            Items = cargoItems
        };

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Ware, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Amount, out int? amount) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
            {
                var cargoItem = new CargoItemData
                {
                    Amount = amount.Value,
                    Ware = ware
                };

                cargoItems.Add(cargoItem);
            }
        }

        return returnValue;
    }
}
