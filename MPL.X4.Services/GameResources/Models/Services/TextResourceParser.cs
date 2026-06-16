using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;

namespace MPL.X4.GameResources.Models.Services;

/// <summary>
/// A class that implements a game resource model loader.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal partial class TextResourceParser(
                                          ILogger<TextResourceParser> logger)
    : ITextResourceParser
{
    [GeneratedRegex(@"\s*\([^()]*\)", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex RemoveParenthesesRegex();
    
    [GeneratedRegex(@"\{(\d+)\s*,\s*(\d+)\}", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex PlaceholderRegex();

    private readonly Dictionary<string, string> _cache = [];
    private readonly Lock _parseLock = new();

    private TextResourceModel CreateModel(int pageId, int textId, string text, ITextResourcePageDictionary textResource)
        => new()
        {
            Id = TextResourceModel.MakeId(pageId, textId),
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

        // Ensures that escaped parenthesis aren't stripped
        text = text.Replace("\\(", "|||").Replace("\\)", "|-|");
        var processed = RemoveParenthesesRegex().Replace(text, "");
        processed = processed.Replace("|||", "(").Replace("|-|", ")");

        processed = PlaceholderRegex().Replace(processed, m =>
        {
            var pageId = int.Parse(m.Groups[1].ValueSpan);
            var entryId = int.Parse(m.Groups[2].ValueSpan);

            if (resources.TryGetValue(pageId, out var page) &&
                page.TryGetValue(entryId, out var entry))
            {
                return RecursiveMapText(entry.Text, resources);
            }
            else
            {
                logger.LogWarning("Unable to map text resource for {PageId},{TextId}", pageId, entryId);
                return $"MISSING_({pageId},{entryId})";
            }
        });

        _cache[text] = processed;

        return processed;
    }

    private string? RecursiveParseText(string? text, ITextResourceModelList resources)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return text;
        }

        return PlaceholderRegex().Replace(text, match =>
        {
            var pageId = int.Parse(match.Groups[1].Value);
            var textId = int.Parse(match.Groups[2].Value);

            var reference = new TextResourceReference(pageId, textId);
            if (resources.TryGetValue(reference, out var textModel))
            {
                var returnValue = RecursiveParseText(textModel.Text, resources);
                if (string.IsNullOrWhiteSpace(returnValue))
                {
                    logger.LogWarning("Unable to parse text resource for {PageId},{TextId}", pageId, textId);
                    return match.Value;
                }

                return returnValue;
            }

            return match.Value;
        });
    }

    string? ITextResourceParser.ParseText(string? text, ITextResourceModelList resources)
        => RecursiveParseText(text, resources);

    string ITextResourceParser.ParseText(string text, ITextResourcePageDictionary resources)
    {
        lock (_parseLock)
        {
            return RecursiveMapText(text, resources);
        }
    }
}
