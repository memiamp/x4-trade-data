using Microsoft.Extensions.DependencyInjection;
using MPL.X4.TradeData.UI.Configuration;
using MPL.X4.TradeData.UI.Forms;
using MPL.X4.TradeData.UI.Services;
using MPL.X4.TradeData.UI.Services.Logging;

namespace MPL.X4.TradeData.UI;

/// <summary>
/// A class that implements the startup for the application.
/// </summary>
internal static class Program
{
    private static IServiceProvider BuildServices()
    {
        var services = new ServiceCollection();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddSimpleConsoleLogger();
        });

        // Configuration
        services.AddSingleton<IFileConfiguration>(new FileConfiguration());

        // Services
        Imports.XmlPatch.Bootstrap.AddServices(services);
        Bootstrap.AddServices(services);

        // Local forms
        services.AddTransient<DebugForm>();
        services.AddTransient<MainForm>();
        services.AddTransient<OptionsForm>();

        // Local services
        services.AddSingleton<IGameDataService, GameDataService>();
        services.AddTransient<ISaveGameFileSystemMonitor, SaveGameFileSystemMonitor>();
        services.AddTransient<ISpecialItemDataProvider, SpecialItemDataProvider>();
        
        return services.BuildServiceProvider();
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        BuildServices().RunApplication<MainForm>();
    }
}
