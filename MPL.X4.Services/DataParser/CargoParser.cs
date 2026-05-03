using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="ICargo"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class CargoParser(
                           ILogger<CargoParser> logger)
    : DataParserBase<ICargo>(logger)
{
    private protected override async Task<ICargo> OnParse(IXmlReaderWrapper reader)
    {
        var cargoItems = new List<ICargoItem>();
        var returnValue = new Cargo
        {
            Items = cargoItems
        };

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Ware, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Amount, out int? amount) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
            {
                var cargoItem = new CargoItem
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
