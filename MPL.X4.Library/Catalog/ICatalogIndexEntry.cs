namespace MPL.X4.Catalog;

/// <summary>
/// An interface that defines a catalog index file entry.
/// </summary>
public interface ICatalogIndexEntry
{
    /// <summary>
    /// Gets the filepath in the catalog.
    /// </summary>
    string FilePath { get; }

    /// <summary>
    /// Gets the file offset.
    /// </summary>
    long Offset { get; }

    /// <summary>
    /// Gets the file signature hash.
    /// </summary>
    string Signature { get; }

    /// <summary>
    /// Gets the file size in bytes.
    /// </summary>
    int Size { get; }

    /// <summary>
    /// Gets the file timestamp.
    /// </summary>
    DateTimeOffset Timestamp { get; }
}
