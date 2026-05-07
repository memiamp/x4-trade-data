using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ITextResourcePage"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="textResourceItemParser">An <see cref="IDataParser{ITextResourceItem}"/> that is the text resource item parser to use.</param>
internal class TextResourcePageParser(
                                      IDataParser dataParser,
                                      ILogger<TextResourcePageParser> logger)
    : DataParserBase<ITextResourcePage>(logger)
{
    private protected override async Task<ITextResourcePage> OnParse(IXmlReaderWrapper reader)
    {
        TextResourcePage? returnValue;

        if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Page, XmlNodeType.Element, 0) &&
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.PageId, out int? id))
        {
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Description, out string? description);
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Title, out string? title);

            returnValue = new()
            {
                Description = description ?? string.Empty,
                Id = id.Value,
                Title = title ?? string.Empty
            };
        }
        else
        {
            logger.LogWarning("Could not load text resource page");
            throw new ArgumentException("Could not load text resource page", nameof(reader));
        }

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.TextEntry, XmlNodeType.Element))
            {
                var textItem = await dataParser.Parse<ITextResourceItem>(reader);
                returnValue.Add(textItem.Id, textItem);
            }
        }

        return returnValue;
    }
}
