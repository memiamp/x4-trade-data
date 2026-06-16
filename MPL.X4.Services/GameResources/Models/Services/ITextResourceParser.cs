using MPL.X4.GameResources.Data;

namespace MPL.X4.GameResources.Models.Services;

/// <summary>
/// An interface that defines the behaviour of a text resource parser.
/// </summary>
public interface ITextResourceParser
{
    /// <summary>
    /// Parses the specified <paramref name="text"/> resource.
    /// </summary>
    /// <param name="text">A nullable <see cref="string"/> containing the text to parse.</param>
    /// <param name="resources">An <see cref="ITextResourceModelList"/> containing the text resources.</param>
    /// <returns>A nullable <see cref="string"/> containing the result.</returns>
    string? ParseText(string? text, ITextResourceModelList resources);

    /// <summary>
    /// Parses the specified <paramref name="text"/> resource.
    /// </summary>
    /// <param name="text">A <see cref="string"/> containing the text to parse.</param>
    /// <param name="resources">An <see cref="ITextResourcePageDictionary"/> containing the text resources.</param>
    /// <returns>A <see cref="string"/> containing the result.</returns>
    string ParseText(string text, ITextResourcePageDictionary resources);
}
