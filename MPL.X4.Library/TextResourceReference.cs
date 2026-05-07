namespace MPL.X4;

/// <summary>
/// A class that implements a X4 text resource reference.
/// </summary>
public class TextResourceReference : ITextResourceReference
{
    private readonly int _pageId;
    private readonly int _textId;

    /// <summary>
    /// Creates a new instance of the <see cref="TextResourceReference"/> class with the specified parameters.
    /// </summary>
    /// <param name="rawReference">A <see cref="string"/> containing the raw resource reference.</param>
    /// <exception cref="ArgumentException">Thrown when the specified <paramref name="rawReference"/> is invalid.</exception>
    public TextResourceReference(string rawReference)
    {
        var parts = rawReference.Trim('{', '}').Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2 ||
            !int.TryParse(parts[0], out _pageId) ||
            !int.TryParse(parts[1], out _textId))
        {
            throw new ArgumentException("The specified raw reference is invalid", nameof(rawReference));
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="TextResourceReference"/> class with the specified parameters.
    /// </summary>
    /// <param name="pageId">An <see cref="int"/> that is the page identifier.</param>
    /// <param name="textId">An <see cref="int"/> that is the text identifier.</param>
    public TextResourceReference(
                                 int pageId,
                                 int textId)
    {
        _pageId = pageId;
        _textId = textId;
    }

    public override string ToString()
        => $"{_pageId},{_textId}";

    int ITextResourceReference.PageId => _pageId;

    int ITextResourceReference.TextId => _textId;
}
