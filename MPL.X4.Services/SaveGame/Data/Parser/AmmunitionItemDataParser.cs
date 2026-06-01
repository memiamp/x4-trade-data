using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IAmmunitionItemData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class AmmunitionItemDataParser(
                                        IDataParser dataParser,
                                        ILogger<AmmunitionItemDataParser> logger)
    : DataParserBase<IAmmunitionItemData>(dataParser, logger)
{
    private protected override Task<IAmmunitionItemData> OnParse(IXmlReaderWrapper reader)
    {
        IAmmunitionItemData returnValue;

        if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Ammunition, XmlNodeType.Element, 0) &&
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro))
        {
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Amount, out int? amount);
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Exact, out int? exact);

            returnValue = new AmmunitionItemData
            {
                Amount = amount ?? 0,
                Exact = exact ?? 0,
                Macro = macro
            };
        }
        else
        {
            logger.LogWarning("Could not load ammunition item");
            throw new ArgumentException("Could not load ammunition item", nameof(reader));
        }

        return Task.FromResult(returnValue);
    }
}
