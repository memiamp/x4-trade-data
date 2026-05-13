namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a text resource model.
/// </summary>
internal class TextResourceModel : ModelWithIdBase, ITextResourceModel
{
    public override string ToString()
        => $"{Id} - {Text}";

    public required int PageId { get; init; }

    public required string Text { get; init; }

    public required int TextId { get; init; }
}
