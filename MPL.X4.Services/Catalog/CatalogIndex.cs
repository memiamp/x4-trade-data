namespace MPL.X4.Catalog;

/// <summary>
/// A class that implements a catalog index.
/// </summary>
internal class CatalogIndex : ICatalogIndex
{
    public override string ToString()
        => $"{GamePack} - {IndexFilePath} - Entries: {Entries.Count()}";

    public required string DataFilePath { get; init; }

    public required IEnumerable<string> Dependencies { get; init; }

    public required IEnumerable<ICatalogIndexEntry> Entries { get; init; }

    public required string GamePack { get; init; }

    public required string IndexFilePath { get; init; }
}
