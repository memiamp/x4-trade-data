using Microsoft.Extensions.DependencyInjection;

namespace MPL.X4.Services;

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
        servicesCollection.AddTransient<IResourceFileReader, ResourceFileReader>();
        servicesCollection.AddTransient<ISaveGameLoader, SaveGameLoader>();
        servicesCollection.AddTransient<IXmlReaderWrapper, XmlReaderWrapper>();
        servicesCollection.AddTransient<IXmlReaderWrapperFactory, XmlReaderWrapperFactory>();
    }
}
