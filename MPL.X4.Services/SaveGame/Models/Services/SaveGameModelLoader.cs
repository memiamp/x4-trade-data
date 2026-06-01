using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Models;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Services;

/// <summary>
/// A class that implements a save game model loader.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="serviceProvider">An <see cref="IServiceProvider"/> that is the service provider to use.</param>
internal class SaveGameModelLoader(
                                   ILogger<SaveGameModelLoader> logger,
                                   IServiceProvider serviceProvider)
    : ISaveGameModelLoader
{
    ISaveGameModels ISaveGameModelLoader.LoadModels(ISaveGameData data, IGameResourceModels gameResources)
    {
        using var scopedServiceProvider = serviceProvider.CreateScope();
        var modelParser = serviceProvider.GetRequiredService<IModelParser>();

        var parsingScope = serviceProvider.GetRequiredService<ISaveGameModelParsingScope>();
        parsingScope.GameResources = gameResources;

        var returnValue = modelParser.Parse<ISaveGameData, ISaveGameModels>(data);

        return returnValue;
    }
}
