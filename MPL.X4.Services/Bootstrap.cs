using Microsoft.Extensions.DependencyInjection;
using MPL.X4.Catalog.Services;
using MPL.X4.GameResources.Data;
using MPL.X4.GameResources.Data.Parser;
using MPL.X4.GameResources.Data.Services;
using MPL.X4.GameResources.Models;
using MPL.X4.GameResources.Models.Parser;
using MPL.X4.GameResources.Models.Services;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;
using MPL.X4.SaveGame.Data.Parser;

namespace MPL.X4;

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
        servicesCollection.AddSingleton<IGameResourceDataAccessor, GameResourceDataAccessor>();
        
        servicesCollection.AddTransient<ICatalogFileReader, CatalogFileReader>();
        servicesCollection.AddTransient<IGameResourceDataLoader, GameResourceDataLoader>();
        servicesCollection.AddTransient<IGameResourceDataParser, GameResourceDataParser>();
        servicesCollection.AddTransient<IGameResourceDataProvider, GameResourceDataProvider>();
        servicesCollection.AddTransient<IGameResourceModelLoader, GameResourceModelLoader>();
        servicesCollection.AddTransient<ISaveGameLoader, SaveGameLoader>();
        servicesCollection.AddTransient<IXmlReaderWrapper, XmlReaderWrapper>();
        servicesCollection.AddTransient<IXmlReaderWrapperFactory, XmlReaderWrapperFactory>();

        // Data parsers
        servicesCollection.AddTransient<IDataParser, X4.Parser.DataParser>();
        servicesCollection.AddTransient<IDataParser<ICargoData>, CargoDataParser>();
        servicesCollection.AddTransient<IDataParser<IColourData>, ColourDataParser>();
        servicesCollection.AddTransient<IDataParser<IColourDataDictionary>, ColourDataDictionaryParser>();
        servicesCollection.AddTransient<IDataParser<IFactionData>, FactionDataParser>();
        servicesCollection.AddTransient<IDataParser<IFactionDataDictionary>, FactionDataDictionaryParser>();
        servicesCollection.AddTransient<IDataParser<IGateData>, GateDataParser>();
        servicesCollection.AddTransient<IDataParser<ILockboxData>, LockboxDataParser>();
        servicesCollection.AddTransient<IDataParser<IMappingData>, MappingDataParser>();
        servicesCollection.AddTransient<IDataParser<IMappingDataDictionary>, MappingDataDictionaryParser>();
        servicesCollection.AddTransient<IDataParser<IOffsetData>, OffsetDataParser>();
        servicesCollection.AddTransient<IDataParser<ISectorData>, SectorDataParser>();
        servicesCollection.AddTransient<IDataParser<IPosition3D>, Position3DParser>();
        servicesCollection.AddTransient<IDataParser<IRotation3D>, Rotation3DParser>();
        servicesCollection.AddTransient<IDataParser<IShipData>, ShipDataParser>();
        servicesCollection.AddTransient<IDataParser<IStationData>, StationDataParser>();
        servicesCollection.AddTransient<IDataParser<ITextResourceItem>, TextResourceItemParser>();
        servicesCollection.AddTransient<IDataParser<ITextResourcePage>, TextResourcePageParser>();
        servicesCollection.AddTransient<IDataParser<ITradeData>, TradeDataParser>();
        servicesCollection.AddTransient<IDataParser<IUniverseData>, UniverseDataParser>();
        servicesCollection.AddTransient<IDataParser<IZoneOffsetData>, ZoneOffsetDataParser>();
        servicesCollection.AddTransient<IDataParser<IZoneOffsetDataDictionary>, ZoneOffsetDataDictionaryParser>();
        //servicesCollection.AddTransient<IDataParser<IZone>, ZoneParser>();
        servicesCollection.AddSingleton<IZoneDataParser, ZoneDataParser>();

        // Model parsers
        servicesCollection.AddScoped<IGameResourceModelParsingScope, GameResourceModelParsingScope>();
        servicesCollection.AddTransient<IModelParser, ModelParser>();
        servicesCollection.AddTransient<IModelParser<IFactionData, IFactionModel>, FactionModelParser>();
        servicesCollection.AddTransient<IModelParser<IFactionDataDictionary, IFactionModelList>, FactionModelListParser>();
        servicesCollection.AddTransient<IModelParser<ColourModelSource, IColourModel>, ColourModelParser>();
        servicesCollection.AddTransient<IModelParser<IColourResourceData, IColourModelList>, ColourModelListParser>();
    }
}
