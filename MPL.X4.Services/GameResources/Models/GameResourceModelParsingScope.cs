using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

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
        var id = $"{source.PageId}|{source.TextId}";

        if (((IGameResourceModelParsingScope)this).TextResources.TryGetValue(id, out var returnValue))
        {
            return returnValue.Text;
        }

        logger.LogWarning("Could not lookup text resource {TextResourceId} in text resource data", id);
        throw new ArgumentException("The specified text resource identifiers are invalid", nameof(source));
    }

    [AllowNull]
    IColourModelList IGameResourceModelParsingScope.Colours { get; set; }

    [AllowNull]
    ITextResourceModelList IGameResourceModelParsingScope.TextResources { get; set; }
}
