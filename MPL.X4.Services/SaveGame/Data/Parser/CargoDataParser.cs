using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ICargoData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class CargoDataParser(
                               ILogger<CargoDataParser> logger)
    : DataParserBase<ICargoData>(logger)
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
