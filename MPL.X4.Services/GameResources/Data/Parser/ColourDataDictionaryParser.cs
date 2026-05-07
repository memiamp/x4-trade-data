using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IColourDataDictionary"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ColourDataDictionaryParser(
                                          IDataParser dataParser,
                                          ILogger<ColourDataDictionaryParser> logger)
    : DataParserBase<IColourDataDictionary>(logger)
{
    private protected override async Task<IColourDataDictionary> OnParse(IXmlReaderWrapper reader)
    {
        ColourDataDictionary returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Colour, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await dataParser.Parse<IColourData>(subtree);

                returnValue.Add(data.Id, data);
            }
        }

        return returnValue;
    }
}
