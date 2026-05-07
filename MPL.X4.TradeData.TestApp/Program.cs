using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data.Services;
using MPL.X4.GameResources.Models.Services;

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
        X4.Bootstrap.AddServices(services);

        return services.BuildServiceProvider();
    }

    private static async Task Main()
    {
        var sp = BuildServices();

        var dataService = sp.GetRequiredService<IGameResourceDataProvider>();
        var resourceData = await dataService.LoadFromCatalog(@"E:\Shared\X4\GameData");
        Console.WriteLine("Resources loaded");

        var modelService = sp.GetRequiredService<IGameResourceModelLoader>();
        var modelData = modelService.LoadGameResourceModels(resourceData);
        Console.WriteLine("Models loaded");
    }
}
