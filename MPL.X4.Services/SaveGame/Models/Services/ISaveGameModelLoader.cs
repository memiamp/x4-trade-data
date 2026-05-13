using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Services;

/// <summary>
/// An interface that defines the behaviour of a save game model loader.
/// </summary>
public interface ISaveGameModelLoader
{
    /// <summary>
    /// Loads save game models from the specified <paramref name="data"/>.
    /// </summary>
    /// <param name="data">An <see cref="ISaveGameData"/> to load models from.</param>
    /// <param name="gameResources">An <see cref="IGameResourceModels"/> containing the game resources.</param>
    /// <returns>An <see cref="ISaveGameModels"/> that is the result.</returns>
    ISaveGameModels LoadModels(ISaveGameData data, IGameResourceModels gameResources);
}
