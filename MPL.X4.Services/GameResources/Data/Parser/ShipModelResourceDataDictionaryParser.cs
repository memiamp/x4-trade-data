using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IShipModelResourceDataDictionary"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ShipModelResourceDataDictionaryParser(
                                                    IDataParser dataParser,
                                                    ILogger<ShipModelResourceDataDictionaryParser> logger)
    : DataParserBase<IShipModelResourceDataDictionary>(dataParser, logger)
{
    private static readonly IEnumerable<string> _shipClasses = [
                                                                Constants.XmlDataFile.AttributeValue.Class.ShipExtraLarge,
                                                                Constants.XmlDataFile.AttributeValue.Class.ShipExtraSmall,
                                                                Constants.XmlDataFile.AttributeValue.Class.ShipLarge,
                                                                Constants.XmlDataFile.AttributeValue.Class.ShipMedium,
                                                                Constants.XmlDataFile.AttributeValue.Class.ShipSmall];

    private protected override async Task<IShipModelResourceDataDictionary> OnParse(IXmlReaderWrapper reader)
    {
        var returnValue = new ShipModelResourceDataDictionary();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Macro, XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, out string? shipClass) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Name, out string? macro))
            {
                // Skip non-ship classes
                if (_shipClasses.Contains(shipClass))
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
