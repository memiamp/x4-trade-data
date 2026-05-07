using MPL.X4.Data;

namespace MPL.X4.Services;

/// <summary>
/// An interface that defines the behaviour of a resource data loader.
/// </summary>
public interface IResourceDataLoader
{
    /// <summary>
    /// Loads faction data from catalogs in the specified <paramref name="catalogsFilePath"/>.
    /// </summary>
    /// <param name="catalogsFilePath">A <see cref="string"/> containing the path to the catalog files.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IFactionsData"/> that is the result.</returns>
    Task<IFactionsData> LoadFactionsFromCatalogs(string catalogsFilePath);
}
