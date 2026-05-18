using Microsoft.Extensions.DependencyInjection;
using MPL.X4.Catalog.Parser;
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
using MPL.X4.SaveGame.Data.Services;
using MPL.X4.SaveGame.Models;
using MPL.X4.SaveGame.Models.Parser;
using MPL.X4.SaveGame.Models.Services;
using MPL.X4.Services.Xml;

namespace MPL.X4;

/// <summary>
/// A class that implements a bootstrap for the assembly.
/// </summary>
public static class Bootstrap
{
    private static void AddCatalogServices(IServiceCollection servicesCollection)
    {
        servicesCollection.AddTransient<ICatalogFileReader, CatalogFileReader>();
    }

    private static void AddGameResourceServices(IServiceCollection servicesCollection)
    {
        // Data model services
        servicesCollection.AddTransient<IGameResourceDataLoader, GameResourceDataLoader>();
        servicesCollection.AddTransient<IGameResourceDataParser, GameResourceDataParser>();
        servicesCollection.AddTransient<IGameResourceDataProvider, GameResourceDataProvider>();

        // Model services
        servicesCollection.AddTransient<IGameResourceModelLoader, GameResourceModelLoader>();
        servicesCollection.AddScoped<IGameResourceModelParsingScope, GameResourceModelParsingScope>();

        // Data model parsers
        servicesCollection.AddTransient<IDataParser<IColourData>, ColourDataParser>();
        servicesCollection.AddTransient<IDataParser<IColourDataDictionary>, ColourDataDictionaryParser>();
        servicesCollection.AddTransient<IDataParser<IFactionData>, FactionDataParser>();
        servicesCollection.AddTransient<IDataParser<IFactionDataDictionary>, FactionDataDictionaryParser>();
        servicesCollection.AddTransient<IDataParser<IMappingData>, MappingDataParser>();
        servicesCollection.AddTransient<IDataParser<IMappingDataDictionary>, MappingDataDictionaryParser>();
        servicesCollection.AddTransient<IDataParser<IOffsetDataDictionary>, OffsetDataDictionaryParser>();
        servicesCollection.AddTransient<IDataParser<IPosition3D>, Position3DParser>();
        servicesCollection.AddTransient<IDataParser<IQuaternion>, QuaternionParser>();
        servicesCollection.AddTransient<IDataParser<IRotation3D>, Rotation3DParser>();
        servicesCollection.AddTransient<IDataParser<ISectorNameResourceDataDictionary>, SectorNameResourceDataDictionaryParser>();
        servicesCollection.AddTransient<IDataParser<IShipModelResourceDataDictionary>, ShipModelResourceDataDictionaryParser>();
        servicesCollection.AddTransient<IDataParser<ITextResourceItem>, TextResourceItemParser>();
        servicesCollection.AddTransient<IDataParser<ITextResourcePage>, TextResourcePageParser>();
        servicesCollection.AddTransient<IDataParser<ITransform3D>, Transform3DParser>();
        servicesCollection.AddTransient<IDataParser<IWareNameResourceDataDictionary>, WareNameResourceDataDictionaryParser>();
        
        // Model parsers
        servicesCollection.AddTransient<IModelParser<IFactionData, IFactionModel>, FactionModelParser>();
        servicesCollection.AddTransient<IModelParser<IFactionDataDictionary, IFactionModelList>, FactionModelListParser>();
        servicesCollection.AddTransient<IModelParser<ColourModelSource, IColourModel>, ColourModelParser>();
        servicesCollection.AddTransient<IModelParser<IColourResourceData, IColourModelList>, ColourModelListParser>();
        servicesCollection.AddTransient<IModelParser<IEnumerable<IOffsetData>, IOffsetModelList>, OffsetModelListParser>();
        servicesCollection.AddTransient<IModelParser<IOffsetData, IOffsetModel>, OffsetModelParser>();
        servicesCollection.AddTransient<IModelParser<IOffsetDataDictionary, IOffsetModelReference>, OffsetModelReferenceParser>();
        servicesCollection.AddTransient<IModelParser<IMacroNameResourceData, IMacroNameModel>, MacroNameModelParser>();
        servicesCollection.AddTransient<IModelParser<IMacroNameResourceDataDictionary, IMacroNameModelList>, MacroNameModelListParser>();
        servicesCollection.AddTransient<IModelParser<ITextResourcePageDictionary, ITextResourceModelList>, TextResourceModelListParser>();
    }

    private static void AddSaveGameServices(IServiceCollection servicesCollection)
    {
        // Save game services
        servicesCollection.AddTransient<ISaveGameDataLoader, SaveGameDataLoader>();

        // Model services
        servicesCollection.AddTransient<ISaveGameModelLoader, SaveGameModelLoader>();
        servicesCollection.AddScoped<ISaveGameModelParsingScope, SaveGameModelParsingScope>();

        // Data parsers
        servicesCollection.AddTransient<IDataParser, X4.Parser.DataParser>();
        servicesCollection.AddTransient<IDataParser<ICargoData>, CargoDataParser>();
        servicesCollection.AddTransient<IDataParser<IGateData>, GateDataParser>();
        servicesCollection.AddTransient<IDataParser<ILockboxData>, LockboxDataParser>();
        servicesCollection.AddTransient<IDataParser<IEnumerable<IModificationData>>, ModificationParser>();
        servicesCollection.AddTransient<IDataParser<ISectorData>, SectorDataParser>();
        servicesCollection.AddTransient<IDataParser<IShipData>, ShipDataParser>();
        servicesCollection.AddTransient<IDataParser<IStationData>, StationDataParser>();
        servicesCollection.AddTransient<IDataParser<ITradeData>, TradeDataParser>();
        servicesCollection.AddTransient<IDataParser<IUniverseData>, UniverseDataParser>();
        servicesCollection.AddTransient<IDataParser<IWareItemData>, WareItemDataParser>();
        servicesCollection.AddTransient<IDataParser<IZoneData>, ZoneDataParser>();

        // Model parsers
        servicesCollection.AddTransient<IModelParser<IWareItemData, ICargoItemModel>, CargoItemModelParser>();
        servicesCollection.AddTransient<IModelParser<IEnumerable<IWareItemData>, ICargoItemModelList>, CargoItemModelListParser>();
        servicesCollection.AddTransient<IModelParser<IEnumerable<ISectorData>, ISectorModelList>, SectorModelListParser>();
        servicesCollection.AddTransient<IModelParser<IGateData, IGateModel>, GateModelParser>();
        servicesCollection.AddTransient<IModelParser<ILockboxData, ILockboxModel>, LockboxModelParser>();
        servicesCollection.AddTransient<IModelParser<IModificationData, IModificationModel>, ModificationModelParser>();
        servicesCollection.AddTransient<IModelParser<IEnumerable<IModificationData>, IModificationModelList>, ModificationModelListParser>();
        servicesCollection.AddTransient<IModelParser<ISaveGameData, ISaveGameModels>, SaveGameModelsParser>();
        servicesCollection.AddTransient<IModelParser<ISectorData, ISectorModel>, SectorModelParser>();
        servicesCollection.AddTransient<IModelParser<IShipData, IShipModel>, ShipModelParser>();
        servicesCollection.AddTransient<IModelParser<IUniverseData, IUniverseModel>, UniverseModelParser>();
    }

    /// <summary>
    /// Adds local services to the specified <paramref name="servicesCollection"/>
    /// </summary>
    /// <param name="servicesCollection">An <see cref="IServiceCollection"/> to add services to.</param>
    public static void AddServices(IServiceCollection servicesCollection)
    {
        // Catalog services
        servicesCollection.AddTransient<IDataParser<ContentFileData>, ContentFileDataParser>();

        // General services
        servicesCollection.AddTransient<IXmlReaderWrapper, XmlReaderWrapper>();
        servicesCollection.AddTransient<IXmlReaderWrapperFactory, XmlReaderWrapperFactory>();

        // Parser services
        servicesCollection.AddTransient<IDataParser, DataParser>();
        servicesCollection.AddTransient<IModelParser, ModelParser>();

        AddCatalogServices(servicesCollection);

        AddGameResourceServices(servicesCollection);

        AddSaveGameServices(servicesCollection);
    }
}
