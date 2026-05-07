using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ITextResourceItem"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class TextResourceItemParser(
                                      ILogger<TextResourceItemParser> logger)
    : DataParserBase<ITextResourceItem>(logger)
{
    private protected override async Task<ITextResourceItem> OnParse(IXmlReaderWrapper reader)
    {
        TextResourceItem? returnValue;

        if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.TextEntry, XmlNodeType.Element, 1) &&
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.TextId, out int? id))
        {
            using var subtree = await reader.ReadSubtree();

            string value = await subtree.ReadElementContentAsStringAsync();

            returnValue = new()
            {
                 Id = id.Value,
                 Text = value
            };
        }
        else
        {
            logger.LogWarning("Could not load text resource item");
            throw new ArgumentException("Could not load text resource item", nameof(reader));
        }

        return returnValue;
    }
}
