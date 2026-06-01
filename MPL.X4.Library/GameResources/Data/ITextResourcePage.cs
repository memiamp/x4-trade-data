namespace MPL.X4.GameResources.Data;

/// <summary>
/// An interface that defines the behaviour of a text resource page.
/// </summary>
public interface ITextResourcePage : IDictionaryCollection<int, ITextResourceItem>
{
    /// <summary>
    /// Gets the description of the text resource page.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Gets the identifier of the text resource page.
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Gets the title of the text resource page.
    /// </summary>
    string Title { get; }
}
