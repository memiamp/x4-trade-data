using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IWareNameResourceDataDictionary"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class WareNameResourceDataDictionaryParser(
                                                      IDataParser dataParser,
                                                      ILogger<WareNameResourceDataDictionaryParser> logger)
    : DataParserBase<IWareNameResourceDataDictionary>(dataParser, logger)
{
    private protected override async Task<IWareNameResourceDataDictionary> OnParse(IXmlReaderWrapper reader)
    {
        var returnValue = new WareNameResourceDataDictionary();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Ware, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Name, out string? name) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.WareId, out string? macro))
            {
                if (TextResourceReference.TryParse(name, out var nameResource))
                {
                    var data = new MacroNameResourceData
                    {
                        Id = macro.ToLower(),
                        NameResource = TextResourceReference.Parse(name)
                    };

                    returnValue[data.Id] = data;
                }
                else
                {
                    Logger.LogInformation("Unable to map ware {WareMacro} as name '{Name}' is not a text resource", macro, name);
                }
            }
        }

        return returnValue;
    }
}
