
namespace MPL.X4.Catalog;

/// <summary>
/// A class that implements a catalog index file entry.
/// </summary>
internal class CatalogIndexEntry : ICatalogIndexEntry
{
    public override string ToString()
        => $"{FilePath} - {Size}";

    public required string FilePath { get; init; }

    public required long Offset { get; init; }

    public required string Signature { get; init; }

    public required int Size { get; init; }

    public required DateTimeOffset Timestamp { get; init; }
}
