namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a text resource model.
/// </summary>
internal class TextResourceModel : ModelWithIdBase, ITextResourceModel
{
    public override string ToString()
        => $"{Id} - {Text}";

    /// <summary>
    /// Makes an identifier for the model from the specified parameters.
    /// </summary>
    /// <param name="pageId">An <see cref="int"/> indicating the page identifier.</param>
    /// <param name="textId">An <see cref="int"/> indicating the text identifier.</param>
    /// <returns>A <see cref="string"/> that is the result.</returns>
    internal static string MakeId(int pageId, int textId)
        => $"{pageId}|{textId}";

    /// <summary>
    /// Makes an identifier for the model from the specified parameters.
    /// </summary>
    /// <param name="textResourceReference">An <see cref="ITextResourceReference"/> that is the text reference.</param>
    /// <returns>A <see cref="string"/> that is the result.</returns>
    internal static string MakeId(ITextResourceReference textResourceReference)
        => MakeId(textResourceReference.PageId, textResourceReference.TextId);

    public required int PageId { get; init; }

    public required string Text { get; init; }

    public required int TextId { get; init; }
}
