using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data.Services;
using MPL.X4.GameResources.Models.Services;
using MPL.X4.SaveGame.Data.Services;
using MPL.X4.SaveGame.Models.Services;

namespace MPL.X4.TradeData.TestApp;

/// <summary>
/// The startup class for the application.
/// </summary>
internal static class Program
{
    private static IServiceProvider BuildServices()
    {
        var services = new ServiceCollection();

        services.AddLogging(lb =>
        {
            lb.AddSimpleConsole();
        });

        // Services
        X4.Imports.XmlPatch.Bootstrap.AddServices(services);
        X4.Bootstrap.AddServices(services);

        return services.BuildServiceProvider();
    }

    private static async Task Main()
    {
        var sp = BuildServices();
        
        var gameResourceDataService = sp.GetRequiredService<IGameResourceDataProvider>();
        var gameResourceData = await gameResourceDataService.LoadFromCatalog(@"E:\Shared\X4\GameData");
        Console.WriteLine("Game resource data loaded");

        var gameResourceModelService = sp.GetRequiredService<IGameResourceModelLoader>();
        var gameResourceModels = gameResourceModelService.LoadModels(gameResourceData);
        Console.WriteLine("Game resource models loaded");
        
        var saveGameService = sp.GetRequiredService<ISaveGameDataLoader>();
        var saveGame = await saveGameService.LoadFrom(@"E:\Shared\X4\save\save_007.xml.gz");
        Console.WriteLine("Save game data loaded");

        var saveGameModelService = sp.GetRequiredService<ISaveGameModelLoader>();
        var saveGameModels = saveGameModelService.LoadModels(saveGame, gameResourceModels);
        Console.WriteLine("Save game models loaded");
    }
}
