using Microsoft.Extensions.DependencyInjection;

namespace MPL.X4.TradeData.Services;

/// <summary>
/// A class that implements a bootstrap for the assembly.
/// </summary>
public static class Bootstrap
{
    /// <summary>
    /// Adds local services to the specified <paramref name="servicesCollection"/>
    /// </summary>
    /// <param name="servicesCollection">An <see cref="IServiceCollection"/> to add services to.</param>
    public static void AddServices(IServiceCollection servicesCollection)
    {
        servicesCollection.AddTransient<IResourceDataLoader, ResourceDataLoader>();
    }
}
