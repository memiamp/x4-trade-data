namespace MPL.X4.TradeData.Services;

/// <summary>
/// An interface that defines the behaviour of a loader of resource data.
/// </summary>
public interface IResourceDataLoader
{
    /// <summary>
    /// Loads resource data from the catalog files int the specified <paramref name="catalogFilePath"/>.
    /// </summary>
    /// <param name="catalogFilePath">A <see cref="string"/> containing the source path of the catalog files to load data from.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. An <see cref="IResourceData"/> that is the result.</returns>
    Task<IResourceData> LoadFromCatalog(string catalogFilePath);
}
