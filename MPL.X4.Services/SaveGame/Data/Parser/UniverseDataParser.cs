using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IUniverseData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class UniverseDataParser(
                                  IDataParser dataParser,
                                  ILogger<UniverseDataParser> logger)
    : DataParserBase<IUniverseData>(dataParser, logger)
{
    private protected override async Task<IUniverseData> OnParse(IXmlReaderWrapper reader)
    {
        IGalaxyData? galaxy = null;

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, x => x == Constants.XmlDataFile.AttributeValue.Class.Galaxy))
            {
                using var subtree = await reader.ReadSubtree();

                galaxy = await DataParser.Parse<IGalaxyData>(subtree);

                break;
            }
        }

        if (galaxy is null)
        {
            logger.LogWarning("Could not parse universe");
            throw new ArgumentException("Could not parse universe", nameof(reader));
        }

        return new UniverseData
        {
            Galaxy = galaxy
        };
    }
}
