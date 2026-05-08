namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines a text resource model.
/// </summary>
public interface ITextResourceModel : IModelWithId
{
    /// <summary>
    /// Gets the identifier of the text page.
    /// </summary>
    int PageId { get; }

    /// <summary>
    /// Gets the text.
    /// </summary>
    string Text { get; }

    /// <summary>
    /// Gets the identifier of the text item.
    /// </summary>
    int TextId { get; }
}
