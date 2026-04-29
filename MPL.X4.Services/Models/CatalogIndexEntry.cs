namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a catalog index file entry.
/// </summary>
internal class CatalogIndexEntry
{
    /// <summary>
    /// Gets the filepath.
    /// </summary>
    internal required string FilePath { get; init; }

    /// <summary>
    /// Gets the file offset.
    /// </summary>
    internal required long Offset { get; init; }

    /// <summary>
    /// Gets the file signature hash.
    /// </summary>
    internal required string Signature { get; init; }

    /// <summary>
    /// Gets the file size in bytes.
    /// </summary>
    internal required int Size { get; init; }

    /// <summary>
    /// Gets the file timestamp.
    /// </summary>
    internal DateTimeOffset Timestamp { get; init; }
}
