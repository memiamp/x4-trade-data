using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IWareDataDictionary"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class WareDataDictionaryParser(
                                        IDataParser dataParser,
                                        ILogger<WareDataDictionaryParser> logger)
    : DataParserBase<IWareDataDictionary>(dataParser, logger)
{
    private protected override async Task<IWareDataDictionary> OnParse(IXmlReaderWrapper reader)
    {
        WareDataDictionary returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Ware, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<IWareData>(subtree);

                returnValue.Add(data.Id, data);
            }
        }

        return returnValue;
    }
}
