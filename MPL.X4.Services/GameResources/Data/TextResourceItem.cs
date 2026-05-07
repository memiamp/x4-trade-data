namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements a data model of a text resource item.
/// </summary>
internal class TextResourceItem : ITextResourceItem
{
    public override string ToString()
        => $"{Id} - {Text}";

    public required int Id { get; init; }

    public required string Text { get; init; }
}
