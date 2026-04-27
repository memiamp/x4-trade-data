using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MPL.X4.TradeData.UI.Forms;
using MPL.X4.TradeData.UI.Services;

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
            loggingBuilder.AddConsole();
            loggingBuilder.AddSimpleConsole();
        });

        // Services
        X4.Services.Bootstrap.AddServices(services);
        MPL.X4.TradeData.Services.Bootstrap.AddServices(services);

        // Local forms
        services.AddTransient<DebugForm>();
        services.AddTransient<MainForm>();

        // Local services
        services.AddTransient<IModelMapper, ModelMapper>();

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
