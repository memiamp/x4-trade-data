namespace MPL.X4.TradeData.Services;

/// <summary>
/// An interface that defines the behaviour of a loader of resource data.
/// </summary>
public interface IResourceDataLoader
{
    /// <summary>
    /// Loads resource data from the specified <paramref name="sourcePath"/>.
    /// </summary>
    /// <param name="sourcePath">A <see cref="string"/> containing the source path to load data from.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. An <see cref="IResourceData"/> that is the result.</returns>
    Task<IResourceData> LoadFrom(string sourcePath);
}
