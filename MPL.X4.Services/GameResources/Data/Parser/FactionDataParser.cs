using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IFactionData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class FactionDataParser(
                                 IDataParser dataParser, 
                                 ILogger<FactionDataParser> logger)
    : DataParserBase<IFactionData>(dataParser, logger)
{
    private protected override async Task<IFactionData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.FactionId, out string? id))
        {
            logger.LogWarning("Could not load faction");
            throw new ArgumentException("Could not load faction", nameof(reader));
        }

        var acronymResource = reader.ParseTextResourceReference(Constants.XmlDataFile.AttributeName.ShortName);
        var nameResource = reader.ParseTextResourceReference(Constants.XmlDataFile.AttributeName.Name);

        var colourReference = await ParseElements(reader);
     
        return new FactionData
        {
            AcronymResource = acronymResource,
            ColourReference = colourReference,
            Id = id,
            NameResource = nameResource
        };
    }

    private static async Task<string?> ParseElements(IXmlReaderWrapper reader)
    {
        string? returnValue = null;

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Colour, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Reference, out returnValue))
            {
                break;
            }
        }

        return returnValue;
    }
}
