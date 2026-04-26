namespace MPL.X4.Services;

/// <summary>
/// An interface that defines the behaviour of a resource file reader.
/// </summary>
public interface IResourceFileReader
{
    /// <summary>
    /// Reads the specified <paramref name="pageId"/> from the resource file at the specified <paramref name="sourcePath"/>.
    /// </summary>
    /// <param name="sourcePath">A <see cref="string"/> containing the path to the resource file.</param>
    /// <param name="pageId">An <see cref="int"/> that is the page identifier to read.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  A <see cref="Dictionary{TKey, TValue}"/> containing the resulting resources.</returns>
    Task<Dictionary<int, string>> ReadPage(string sourcePath, int pageId);

    /// <summary>
    /// Reads the specified <paramref name="pageIds"/> from the resource file at the specified <paramref name="sourcePath"/>.
    /// </summary>
    /// <param name="sourcePath">A <see cref="string"/> containing the path to the resource file.</param>
    /// <param name="pageIds">A <see langword="params"/> array of <see cref="int"/> that are the page identifier to read.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  A <see cref="Dictionary{TKey, TValue}"/> containing the resulting resources.</returns>
    Task<Dictionary<int, Dictionary<int, string>>> ReadPages(string sourcePath, params int[] pageIds);
}
