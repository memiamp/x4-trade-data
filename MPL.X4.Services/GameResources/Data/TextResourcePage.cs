namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements a data model of a text resource page.
/// </summary>
internal class TextResourcePage : DictionaryCollection<int, ITextResourceItem>, ITextResourcePage
{
    /// <summary>
    /// Clones the class to a new instance but does not copy contained items.
    /// </summary>
    /// <param name="source">A <see cref="ITextResourcePage"/> to clone from.</param>
    /// <returns>A <see cref="TextResourcePage"/> that is the result.</returns>
    internal static TextResourcePage Clone(ITextResourcePage source)
        => new()
        {
            Description = source.Description,
            Id = source.Id,
            Title = source.Title,
        };

    public override string ToString()
        => $"{Id} ({Title}) - Entries: {Count}";

    public required string Description { get; init; }

    public required int Id { get; init; }

    public required string Title { get; init; }
}
