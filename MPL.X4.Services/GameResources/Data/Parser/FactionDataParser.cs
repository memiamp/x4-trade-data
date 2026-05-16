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

        ITextResourceReference? nameResource = reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Name, out string? value)
            ? TextResourceReference.Parse(value)
            : null;

        ITextResourceReference? acronymResource = reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.ShortName, out value)
            ? TextResourceReference.Parse(value)
            : null;

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
        string? colourReference = null;

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Colour, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Reference, out colourReference))
            {
                break;
            }
        }

        return colourReference;
    }
}
