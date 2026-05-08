using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IMappingDataDictionary"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class MappingDataDictionaryParser(
                                           IDataParser dataParser,
                                           ILogger<MappingDataDictionaryParser> logger)
    : DataParserBase<IMappingDataDictionary>(logger)
{
    private protected override async Task<IMappingDataDictionary> OnParse(IXmlReaderWrapper reader)
    {
        MappingDataDictionary returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Mapping, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await dataParser.Parse<IMappingData>(subtree);

                returnValue.Add(data.Id, data);
            }
        }

        return returnValue;
    }
}
