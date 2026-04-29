using Microsoft.Extensions.DependencyInjection;
using MPL.X4.Services.DataParser;

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
        servicesCollection.AddTransient<ICatalogFileReader, CatalogFileReader>();
        servicesCollection.AddTransient<IResourceFileReader, ResourceFileReader>();
        servicesCollection.AddTransient<ISaveGameLoader, SaveGameLoader>();
        servicesCollection.AddTransient<IXmlReaderWrapper, XmlReaderWrapper>();
        servicesCollection.AddTransient<IXmlReaderWrapperFactory, XmlReaderWrapperFactory>();

        // Data parsers
        servicesCollection.AddTransient<IDataParser<IGate>, GateParser>();
        servicesCollection.AddTransient<IDataParser<ILockbox>, LockboxParser>();
        servicesCollection.AddTransient<IDataParser<IOffset>, OffsetParser>();
        servicesCollection.AddTransient<IDataParser<ISector>, SectorParser>();
        servicesCollection.AddTransient<IDataParser<ISectorPosition>, SectorPositionParser>();
        servicesCollection.AddTransient<IDataParser<ISectorRotation>, SectorRotationParser>();
        servicesCollection.AddTransient<IDataParser<IShip>, ShipParser>();
        servicesCollection.AddTransient<IDataParser<IStation>, StationParser>();
        servicesCollection.AddTransient<IDataParser<ITrade>, TradeParser>();
        servicesCollection.AddTransient<IDataParser<IUniverse>, UniverseParser>();
        servicesCollection.AddSingleton<IDataParser<IZoneOffset>, ZoneOffsetParser>();

        //servicesCollection.AddTransient<IDataParser<IZone>, ZoneParser>();
        servicesCollection.AddSingleton<IZoneParser, ZoneParser>();
    }
}
