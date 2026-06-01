namespace MPL.X4.GameResources.Data;

/// <summary>
/// An interface that defines the behaviour of a text resource item.
/// </summary>
public interface ITextResourceItem
{
    /// <summary>
    /// Gets the identifier of the text resource.
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Gets the text of the resource.
    /// </summary>
    string Text { get; }
}
