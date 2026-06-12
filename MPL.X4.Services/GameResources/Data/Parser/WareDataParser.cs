using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.GameResources.Data.Parser;

using WareElements = (
                      string? ComponentReference,
                      int PriceAverage,
                      int PriceMaximum,
                      int PriceMinimum);

/// <summary>
/// A class that implements a data parser for an <see cref="IWareData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class WareDataParser(
                              IDataParser dataParser,
                              ILogger<WareDataParser> logger)
    : DataParserBase<IWareData>(dataParser, logger)
{
    private protected override async Task<IWareData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.WareId, out string? id))
        {
            logger.LogWarning("Could not parse ware");
            throw new ArgumentException("Could not parse ware", nameof(reader));
        }

        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Group, out string? group);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Transport, out string? transport);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Volume, out int? volume);

        reader.TryParseTextResourceReference(Constants.XmlDataFile.AttributeName.FactoryName, out var factoryResource);
        reader.TryParseTextResourceReference(Constants.XmlDataFile.AttributeName.Name, out var nameResource);

        var (componentReference, priceAverage, priceMaximum, priceMinimum) = await ParseElements(reader);

        return new WareData
        {
            ComponentReference = componentReference,
            FactoryNameResource = factoryResource,
            Group = group ?? "",
            Id = id,
            NameResource = nameResource,
            PriceAverage = priceAverage,
            PriceMaximum = priceMaximum,
            PriceMinimum = priceMinimum,
            Transport = transport ?? "",
            Volume = volume ?? 0
        };
    }

    private static async Task<WareElements> ParseElements(IXmlReaderWrapper reader)
    {
        int priceAverage = 0;
        int priceMaximum = 0;
        int priceMinimum = 0;
        string? componentReference = null;

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element) &&
                componentReference is null)
            {
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Reference, out componentReference);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Price, XmlNodeType.Element))
            {
                if (reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Average, out int? value))
                {
                    priceAverage = value.Value;
                }

                if (reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Maximum, out value))
                {
                    priceMaximum = value.Value;
                }

                if (reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Minimum, out value))
                {
                    priceMinimum = value.Value;
                }
            }
        }

        return (componentReference, priceAverage, priceMaximum, priceMinimum);
    }
}
