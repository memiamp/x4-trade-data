using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IZoneOffsetDataDictionary"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ZoneOffsetDataDictionaryParser(
                                              IDataParser dataParser,
                                              ILogger<ZoneOffsetDataDictionaryParser> logger)
    : DataParserBase<IZoneOffsetDataDictionary>(logger)
{
    private protected override async Task<IZoneOffsetDataDictionary> OnParse(IXmlReaderWrapper reader)
    {
        ZoneOffsetDataDictionary returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Connection, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.Ref, x => x == Constants.ResourceFile.AttributeValue.Ref.Zones, out string? _))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await dataParser.Parse<IZoneOffsetData>(subtree);

                returnValue.Add(data.MacroName, data);
            }
        }

        return returnValue;
    }
}
