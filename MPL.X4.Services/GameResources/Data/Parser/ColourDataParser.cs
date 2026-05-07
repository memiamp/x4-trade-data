using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IColourData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ColourDataParser(
                                ILogger<ColourDataParser> logger)
    : DataParserBase<IColourData>(logger)
{
    private protected override Task<IColourData> OnParse(IXmlReaderWrapper reader)
    {
        ColourData? returnValue;

        if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Colour, XmlNodeType.Element) &&
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.ColourId, out string? id))
        {
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.ColourAlpha, out int? alpha);
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.ColourBlue, out int? blue);
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.ColourGlow, out int? glow);
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.ColourGreen, out int? green);
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.ColourRed, out int? red);

            returnValue = new ColourData
            {
                Alpha = alpha ?? 0,
                Blue = blue ?? 0,
                Glow = glow ?? 0,
                Green = green ?? 0,
                Id = id,
                Red = red ?? 0
            };
        }
        else
        {
            logger.LogWarning("Could not load colour");
            throw new ArgumentException("Could not load colour", nameof(reader));
        }

        return Task.FromResult<IColourData>(returnValue);
    }
}
