using System.Diagnostics.CodeAnalysis;

namespace MPL.X4;

/// <summary>
/// A class that implements a X4 text resource reference.
/// </summary>
/// <param name="PageId">An <see cref="int"/> indicating the page identifier of the resource.</param>
/// <param name="TextId">An <see cref="int"/> indicating the text identifier of the resource.</param>
public record struct TextResourceReference(int PageId, int TextId) : ITextResourceReference
{
    /// <summary>
    /// Parses the specified <paramref name="rawReference"/> as a <see cref="TextResourceReference"/>.
    /// </summary>
    /// <param name="rawReference">A <see cref="string"/> containing the raw reference text.</param>
    /// <returns>A <see cref="TextResourceReference"/> that is the result.</returns>
    /// <exception cref="ArgumentException">Thrown when the specified <paramref name="rawReference"/> is invalid.</exception>
    public static TextResourceReference Parse(string rawReference)
    {
        if (!TryParse(rawReference, out var returnValue))
        {
            throw new ArgumentException("The specified value is not a valid text resource reference", nameof(rawReference));
        }

        return returnValue.Value;
    }

    public override readonly string ToString() => $"{PageId},{TextId}";

    /// <summary>
    /// Tries to parse the specified <paramref name="rawReference"/> as a <see cref="TextResourceReference"/>.
    /// </summary>
    /// <param name="rawReference">A <see cref="string"/> containing the raw reference text.</param>
    /// <param name="value">A nullable <see cref="TextResourceReference"/> that will be set to the parsed value, or <see langword="null"/>.</param>
    /// <returns>A <see cref="bool"/> indicating success.</returns>
    public static bool TryParse(string rawReference, [NotNullWhen(true)] out TextResourceReference? value)
    {
        var parts = rawReference
                                .Trim(Constants.TextResource.TrimChars)
                                .Split(Constants.TextResource.SplitChar, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 2 &&
            int.TryParse(parts[0], out int pageId) &&
            int.TryParse(parts[1], out int textId))
        {
            value = new TextResourceReference(pageId, textId);
        }
        else
        {
            value = null;
        }

        return value is not null;
    }
}
