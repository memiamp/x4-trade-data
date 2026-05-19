using MPL.X4.GameResources.Data;
using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Services;

/// <summary>
/// An interface that defines the behaviour of a game data service.
/// </summary>
internal interface IGameDataService
{
    /// <summary>
    /// Gets the game resource data.
    /// </summary>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.  An <see cref="IGameResourceData"/> that is the result.</returns>
    ValueTask<IGameResourceData> GetResourceData();

    /// <summary>
    /// Gets the game resource modeles.
    /// </summary>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.  An <see cref="IGameResourceModels"/> that is the result.</returns>
    ValueTask<IGameResourceModels> GetResourceModels();

    /// <summary>
    /// Loads save game data from the specified <paramref name="saveGamePath"/>.
    /// </summary>
    /// <param name="saveGamePath">A <see cref="string"/> containing the full path to the save game.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  A <see cref="ISaveGameModels"/> that is the result.</returns>
    Task<ISaveGameModels> LoadSaveGame(string saveGamePath);

    /// <summary>
    /// Reloads game resources from the path in the configuration.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operaiton.</returns>
    Task ReloadResources();

    /// <summary>
    /// Reloads game resources from the specified <paramref name="resourcePath"/>.
    /// </summary>
    /// <param name="resourcePath">A <see cref="string"/> containing the path to the resources to load.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operaiton.</returns>
    Task ReloadResources(string resourcePath);
}
