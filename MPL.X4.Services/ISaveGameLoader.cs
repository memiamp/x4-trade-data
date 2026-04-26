namespace MPL.X4.Services;

/// <summary>
/// An interface that defines the behaviour of a loader of save games.
/// </summary>
public interface ISaveGameLoader
{
    /// <summary>
    /// Loads a save game from the specified <paramref name="sourcePath"/>.
    /// </summary>
    /// <param name="sourcePath">A <see cref="string"/> containing the source path to load data from.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. An <see cref="ISaveGame"/> that is the result.</returns>
    Task<ISaveGame> LoadFrom(string sourcePath);
}
