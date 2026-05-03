using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Data;
using MPL.X4.Services.Data;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="IFactionData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class FactionParser(
                             ILogger<FactionParser> logger)
    : DataParserBase<IFactionData>(logger)
{
    private protected override async Task<IFactionData> OnParse(IXmlReaderWrapper reader)
    {
        FactionData? returnValue;

        if (reader.TryGetAttribute(Constants.ResourceFile.AttributeName.FactionId, out string? id))
        {
            reader.TryGetAttribute(Constants.ResourceFile.AttributeName.Name, out string? name);
            reader.TryGetAttribute(Constants.ResourceFile.AttributeName.ShortName, out string? shortName);

            returnValue = new FactionData
            {
                AcronymResource = shortName is not null ? new TextResourceReference(shortName) : null,
                Id = id,
                NameResource = name is not null ? new TextResourceReference(name) : null
            };
        }
        else
        {
            logger.LogWarning("Could not load faction");
            throw new ArgumentException("Could not load faction", nameof(reader));
        }

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Colour, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.Ref, out string? colourReference))
            {
                returnValue.ColourReference = colourReference;
                break;
            }
        }

        return returnValue;
    }
}
