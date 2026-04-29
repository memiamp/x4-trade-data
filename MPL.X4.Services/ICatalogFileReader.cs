namespace MPL.X4.Services;

/// <summary>
/// An interface that defines the behaviour of a X4 catalog file reader.
/// </summary>
public interface ICatalogFileReader
{
    /// <summary>
    /// Reads the first file matching the specific <paramref name="fileFilter"/> from the catalog with the matching <paramref name="catalogId"/> as text.
    /// </summary>
    /// <param name="catalogPath">A <see cref="string"/> containing the path to the folder containing catalog files.</param>
    /// <param name="catalogId">An <see cref="int"/> indicating the identifier of the catalog file.</param>
    /// <param name="fileFilter">A <see cref="string"/> containing the file filter to match files with.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  A <see cref="string"/> containing the result.</returns>
    Task<string> ReadTextFile(string catalogPath, int catalogId, string fileFilter);
}
