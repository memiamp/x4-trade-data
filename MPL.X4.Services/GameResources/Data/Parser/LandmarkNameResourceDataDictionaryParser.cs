using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ILandmarkNameResourceDataDictionary"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class LandmarkNameResourceDataDictionaryParser(
                                                        IDataParser dataParser,
                                                        ILogger<LandmarkNameResourceDataDictionaryParser> logger)
    : DataParserBase<ILandmarkNameResourceDataDictionary>(dataParser, logger)
{
    private protected override async Task<ILandmarkNameResourceDataDictionary> OnParse(IXmlReaderWrapper reader)
    {
        var returnValue = new LandmarkNameResourceDataDictionary();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Macro, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Name, out string? macro))
            {
                using var subtree = await reader.ReadSubtree();

                var name = await ReadName(subtree);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var data = new MacroNameResourceData
                    {
                        Id = macro.ToLower(),
                        NameResource = TextResourceReference.Parse(name)
                    };

                    returnValue[data.Id] = data;
                }
            }
        }

        return returnValue;
    }

    private static async Task<string> ReadName(IXmlReaderWrapper reader)
    {
        var returnValue = string.Empty;

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Identification, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Name, out string? name))
            {
                returnValue = name;
                break;
            }
        }

        return returnValue;
    }
}
