using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Data.Services;

/// <summary>
/// An interface that defines the behaviour of a loader of save game data.
/// </summary>
public interface ISaveGameDataLoader
{
    /// <summary>
    /// Loads a save game from the specified <paramref name="sourcePath"/>.
    /// </summary>
    /// <param name="sourcePath">A <see cref="string"/> containing the source path to load data from.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. An <see cref="ISaveGameData"/> that is the result.</returns>
    Task<ISaveGameData> LoadFrom(string sourcePath);
}
