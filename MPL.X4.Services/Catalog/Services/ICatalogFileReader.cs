namespace MPL.X4.Services;

/// <summary>
/// An interface that defines the behaviour of a X4 catalog file reader.
/// </summary>
public interface ICatalogFileReader
{
    /// <summary>
    /// Parses the index of the specified <paramref name="catalogId"/> from <paramref name="catalogPath"/> and optionally filters it by <paramref name="fileFilter"/>.
    /// </summary>
    /// <param name="catalogPath">A <see cref="string"/> containing the path to the folder containing catalog files.</param>
    /// <param name="catalogId">An <see cref="int"/> indicating the identifier of the catalog file.</param>
    /// <param name="fileFilter">A nullable <see cref="string"/> containing the file filter to match files with.</param>
    /// <param name="fileExtension">A nullable <see cref="string"/> containing the file extension to match files with.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  Am <see cref="IEnumerable{T}"/> of <see cref="ICatalogIndexEntry"/> containing the result.</returns>
    Task<IEnumerable<ICatalogIndexEntry>> ParseIndex(string catalogPath, int catalogId, string? fileFilter = null, string? fileExtension = null);

    /// <summary>
    /// Parses the indexes of all catalogs in <paramref name="catalogPath"/> and optionally filters them by <paramref name="fileFilter"/>.
    /// </summary>
    /// <param name="catalogPath">A <see cref="string"/> containing the path to the folder containing catalog files.</param>
    /// <param name="fileFilter">A nullable <see cref="string"/> containing the file filter to match files with.</param>
    /// <param name="fileExtension">A nullable <see cref="string"/> containing the file extension to match files with.</param>
    /// <param name="recursiveSearch">A <see cref="bool"/> indicating whether to recursively search for catalog files under <paramref name="catalogPath"/>.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  Am <see cref="IEnumerable{T}"/> of <see cref="ICatalogIndexEntry"/> containing the result.</returns>
    Task<IEnumerable<ICatalogIndexEntry>> ParseIndexes(string catalogPath, string? fileFilter = null, string? fileExtension = null, bool recursiveSearch = false);

    /// <summary>
    /// Reads the first file matching the specific <paramref name="fileFilter"/> from the catalog with the matching <paramref name="catalogId"/> as text.
    /// </summary>
    /// <param name="catalogPath">A <see cref="string"/> containing the path to the folder containing catalog files.</param>
    /// <param name="catalogId">An <see cref="int"/> indicating the identifier of the catalog file.</param>
    /// <param name="fileFilter">A <see cref="string"/> containing the file filter to match files with.</param>
    /// <param name="fileExtension">A nullable <see cref="string"/> containing the file extension to match the file with.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  A <see cref="string"/> containing the result.</returns>
    Task<string> ReadTextFile(string catalogPath, int catalogId, string fileFilter, string? fileExtension = null);

    /// <summary>
    /// Reads the specified <paramref name="entries"/> as text files.
    /// </summary>
    /// <param name="entries">An <see cref="IEnumerable{T}"/> of <see cref="ICatalogIndexEntry"/> that are the entries to read.</param>
    /// <param name="cancellationToken">A nullable <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>An <see cref="IAsyncEnumerable{T}"/> of type <see cref="string"/> that is the result.</returns>
    IAsyncEnumerable<string> ReadTextFiles(IEnumerable<ICatalogIndexEntry> entries, CancellationToken cancellationToken = default);
}
