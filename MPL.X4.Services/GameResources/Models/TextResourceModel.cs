namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a text resource model.
/// </summary>
internal class TextResourceModel : ModelWithIdBase, ITextResourceModel
{
    /// <summary>
    /// Gets the default model.
    /// </summary>
    /// <returns>An <see cref="ITextResourceModel"/> that is the result.</returns>
    internal static ITextResourceModel GetDefault()
        => new TextResourceModel
        {
            PageId = 0,
            Id = string.Empty,
            Text = string.Empty,
            TextId = 0
        };

    public override string ToString()
        => $"{Id} - {Text}";

    public required int PageId { get; init; }

    public required string Text { get; init; }

    public required int TextId { get; init; }
}
