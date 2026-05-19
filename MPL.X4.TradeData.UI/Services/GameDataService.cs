using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.GameResources.Data.Services;
using MPL.X4.GameResources.Models;
using MPL.X4.GameResources.Models.Services;
using MPL.X4.SaveGame.Data.Services;
using MPL.X4.SaveGame.Models;
using MPL.X4.SaveGame.Models.Services;
using MPL.X4.TradeData.UI.Configuration;

namespace MPL.X4.TradeData.UI.Services;

/// <summary>
/// A class that implements a game data service.
/// </summary>
/// <param name="fileConfiguration">An <see cref="IFileConfiguration"/> that is the file configuration to use.</param>
/// <param name="gameResourceDataProvider">An <see cref="IGameResourceDataProvider"/> that is the game resource data provider.</param>
/// <param name="gameResourceModelLoader">An <see cref="IGameResourceModelLoader"/> that is the game resource model loader.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="saveGameDataLoader">An <see cref="ISaveGameDataLoader"/> that is the save game data loader.</param>
/// <param name="saveGameModelLoader">An <see cref="ISaveGameModelLoader"/> that is the save game model loader.</param>
internal class GameDataService(
                               IFileConfiguration fileConfiguration,
                               IGameResourceDataProvider gameResourceDataProvider,
                               IGameResourceModelLoader gameResourceModelLoader,
                               ILogger<GameDataService> logger,
                               ISaveGameDataLoader saveGameDataLoader,
                               ISaveGameModelLoader saveGameModelLoader)
    : IGameDataService
{
    [AllowNull]
    private IGameResourceData _gameResourceData;
    [AllowNull]
    private IGameResourceModels _gameResourceModels;

    async ValueTask<IGameResourceData> IGameDataService.GetResourceData()
    {
        if (_gameResourceData is null)
        {
            await ((IGameDataService)this).ReloadResources();
        }

        if (_gameResourceData is null)
        {
            logger.LogWarning("Unable to load game resource data");
            throw new InvalidOperationException("Unable to load game resource data");
        }

        return _gameResourceData;
    }

    async ValueTask<IGameResourceModels> IGameDataService.GetResourceModels()
    {
        if (_gameResourceModels is null)
        {
            await ((IGameDataService)this).ReloadResources();
        }

        if (_gameResourceModels is null)
        {
            logger.LogWarning("Unable to load game resource models");
            throw new InvalidOperationException("Unable to load game resource models");
        }

        return _gameResourceModels;
    }

    async Task<ISaveGameModels> IGameDataService.LoadSaveGame(string saveGamePath)
    {
        var models = await ((IGameDataService)this).GetResourceModels();

        var saveGameData = await saveGameDataLoader.LoadFrom(saveGamePath);

        return saveGameModelLoader.LoadModels(saveGameData, models);
    }

    Task IGameDataService.ReloadResources()
        => ((IGameDataService)this).ReloadResources(fileConfiguration.CatalogFilePath);

    async Task IGameDataService.ReloadResources(string resourcePath)
    {
        logger.LogInformation("Reloading game resources from {ResourcePath}", resourcePath);

        _gameResourceData = await gameResourceDataProvider.LoadFromCatalog(resourcePath);
        _gameResourceModels = gameResourceModelLoader.LoadModels(_gameResourceData);
    }

}
