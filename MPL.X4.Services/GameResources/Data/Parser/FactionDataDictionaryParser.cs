using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IFactionDataDictionary"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class FactionDataDictionaryParser(
                                           IDataParser dataParser,
                                           ILogger<FactionDataDictionaryParser> logger)
    : DataParserBase<IFactionDataDictionary>(logger)
{
    private protected override async Task<IFactionDataDictionary> OnParse(IXmlReaderWrapper reader)
    {
        FactionDataDictionary returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Faction, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await dataParser.Parse<IFactionData>(subtree);

                returnValue.Add(data.Id, data);
            }
        }

        return returnValue;
    }
}
