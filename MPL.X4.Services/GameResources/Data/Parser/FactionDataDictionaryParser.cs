using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Data;
using MPL.X4.Services.Data;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="IFactionsData"/>.
/// </summary>
/// <param name="factionParser">An <see cref="IDataParser{IFactionData}"/> that is the faction parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class FactionsParser(
                              IDataParser<IFactionData> factionParser,
                              ILogger<FactionsParser> logger)
    : DataParserBase<IFactionsData>(logger)
{
    private protected override async Task<IFactionsData> OnParse(IXmlReaderWrapper reader)
    {
        FactionsData returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Faction, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.FactionId, out string? id))
            {
                using var factionSubTree = await reader.ReadSubtree();

                var faction = await factionParser.Parse(factionSubTree);

                returnValue.Add(id, faction);
            }
        }

        return returnValue;
    }
}
