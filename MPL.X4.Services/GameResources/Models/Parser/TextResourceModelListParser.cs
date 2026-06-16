using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.GameResources.Models.Services;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to an <see cref="ITextResourceModelList"/> from an <see cref="ITextResourcePageDictionary"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="textResourceParser">An <see cref="ITextResourceParser"/> that is the text resource parser.</param>
internal partial class TextResourceModelListParser(
                                                   ILogger<TextResourceModelListParser> logger,
                                                   ITextResourceParser textResourceParser)
    : IModelParser<ITextResourcePageDictionary, ITextResourceModelList>
{
    private readonly Lock _parseLock = new();

    private TextResourceModel CreateModel(int pageId, int textId, string text, ITextResourcePageDictionary textResource)
        => new()
        {
            Id = TextResourceModel.MakeId(pageId, textId),
            PageId = pageId,
            Text = textResourceParser.ParseText(text, textResource),
            TextId = textId
        };

    ITextResourceModelList IModelParser<ITextResourcePageDictionary, ITextResourceModelList>.Parse(ITextResourcePageDictionary source)
    {
        TextResourceModelList returnValue = [];

        lock (_parseLock)
        {
            foreach (var (_, page) in source)
            {
                foreach (var (_, text) in page)
                {
                    returnValue.Add(CreateModel(page.Id, text.Id, text.Text, source));
                }
            }
        }

        return returnValue;
    }
}
