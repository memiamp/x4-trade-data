using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to an <see cref="ITextResourceModelList"/> from an <see cref="ITextResourcePageDictionary"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal partial class TextResourceModelListParser(
                                                   ILogger<TextResourceModelListParser> logger)
    : IModelParser<ITextResourcePageDictionary, ITextResourceModelList>
{
    [GeneratedRegex(@"\s*\([^()]*\)", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex RemoveParenthesesRegex();

    [GeneratedRegex(@"\{(\d+),(\d+)\}", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex PlaceholderRegex();

    private readonly Dictionary<string, string> _cache = [];
    private readonly Lock _parseLock = new();

    private TextResourceModel CreateModel(int pageId, int textId, string text, ITextResourcePageDictionary textResource)
        => new()
        {
            Id = $"{pageId}|{textId}",
            PageId = pageId,
            Text = RecursiveMapText(text, textResource),
            TextId = textId
        };

    private string RecursiveMapText(string text, ITextResourcePageDictionary resources)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return text;
        }

        if (_cache.TryGetValue(text, out var cached))
        {
            return cached;
        }

        var processed = RemoveParenthesesRegex().Replace(text, "");
        processed = PlaceholderRegex().Replace(processed, m =>
        {
            var pageId = int.Parse(m.Groups[1].ValueSpan);
            var entryId = int.Parse(m.Groups[2].ValueSpan);

            return resources.TryGetValue(pageId, out var page) &&
                   page.TryGetValue(entryId, out var entry)
                ? RecursiveMapText(entry.Text, resources)
                : $"MISSING_({pageId},{entryId})";
        });

        _cache[text] = processed;

        return processed;
    }

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

            _cache.Clear();
        }

        return returnValue;
    }
}
