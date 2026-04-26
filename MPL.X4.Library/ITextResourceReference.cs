namespace MPL.X4;

/// <summary>
/// An interface that defines a X4 text resource reference.
/// </summary>
public interface ITextResourceReference
{
    /// <summary>
    /// Gets the identifier of the resource page.
    /// </summary>
    public int PageId { get; }

    /// <summary>
    /// Gets the identifier of the text entry.
    /// </summary>
    public int TextId { get; }
}
