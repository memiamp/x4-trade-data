using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;

namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a scope for parsing game resource models.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class GameResourceModelParsingScope(
                                             ILogger<GameResourceModelParsingScope> logger)
    : IGameResourceModelParsingScope
{
    string IGameResourceModelParsingScope.Lookup(ITextResourceReference source)
    {
        if (((IGameResourceModelParsingScope)this).TextResources.TryGetValue(source.PageId, out var page))
        {
            if (page.TryGetValue(source.TextId, out var returnValue))
            {
                return returnValue.Text;
            }

            logger.LogWarning("Could not find text entry {TextId} in page {PageId} in text resource data", source.TextId, source.PageId);
            throw new ArgumentException("The specified text identifier is invalid", nameof(source));
        }

        logger.LogWarning("Could not find page {PageId} in text resource data", source.PageId);
        throw new ArgumentException("The specified page identifier is invalid", nameof(source));
    }

    [AllowNull]
    IColourModelList IGameResourceModelParsingScope.Colours { get; set; }

    [AllowNull]
    ITextResourcePageDictionary IGameResourceModelParsingScope.TextResources { get; set; }
}
