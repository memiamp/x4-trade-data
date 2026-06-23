using MPL.X4.Catalog;

namespace MPL.X4.GameResources.Data.Services;

/// <summary>
/// An interface that defines the behaviour of a game resource data loader.
/// </summary>
public interface IGameResourceDataLoader
{
    /// <summary>
    /// Loads colour resource data from catalogs in the specified <paramref name="catalogsFilePath"/>.
    /// </summary>
    /// <param name="catalogsFilePath">A <see cref="string"/> containing the path to the catalog files.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IColourResourceData"/> that is the result.</returns>
    Task<IColourResourceData> LoadColourResourcesFromCatalogs(string catalogsFilePath);

    /// <summary>
    /// Loads colour data from entries in the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">An <see cref="IEnumerable{ICatalogIndex}"/> that is the catalog file index.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IColourResourceData"/> that is the result.</returns>
    Task<IColourResourceData> LoadColourResourcesFromIndex(IEnumerable<ICatalogIndex> index);

    /// <summary>
    /// Loads faction data from catalogs in the specified <paramref name="catalogsFilePath"/>.
    /// </summary>
    /// <param name="catalogsFilePath">A <see cref="string"/> containing the path to the catalog files.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IFactionDataDictionary"/> that is the result.</returns>
    Task<IFactionDataDictionary> LoadFactionsFromCatalogs(string catalogsFilePath);

    /// <summary>
    /// Loads faction data from entries in the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">An <see cref="IEnumerable{ICatalogIndex}"/> that is the catalog file index.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IFactionDataDictionary"/> that is the result.</returns>
    Task<IFactionDataDictionary> LoadFactionsFromIndex(IEnumerable<ICatalogIndex> index);

    /// <summary>
    /// Loads landmark names from catalogs in the specified <paramref name="catalogsFilePath"/>.
    /// </summary>
    /// <param name="catalogsFilePath">A <see cref="string"/> containing the path to the catalog files.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IMacroNameResourceDataDictionary"/> that is the result.</returns>
    Task<IMacroNameResourceDataDictionary> LoadLandmarkNamesFromCatalogs(string catalogsFilePath);

    /// <summary>
    /// Loads landmark names from entries in the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">An <see cref="IEnumerable{ICatalogIndex}"/> that is the catalog file index.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IMacroNameResourceDataDictionary"/> that is the result.</returns>
    Task<IMacroNameResourceDataDictionary> LoadLandmarkNamesFromIndex(IEnumerable<ICatalogIndex> index);

    /// <summary>
    /// Loads offset data from catalogs in the specified <paramref name="catalogsFilePath"/>.
    /// </summary>
    /// <param name="catalogsFilePath">A <see cref="string"/> containing the path to the catalog files.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IOffsetDataDictionary"/> that is the result.</returns>
    Task<IOffsetDataDictionary> LoadOffsetsFromCatalogs(string catalogsFilePath);

    /// <summary>
    /// Loads offset data from entries in the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">An <see cref="IEnumerable{ICatalogIndex}"/> that is the catalog file index.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IOffsetDataDictionary"/> that is the result.</returns>
    Task<IOffsetDataDictionary> LoadOffsetsFromIndex(IEnumerable<ICatalogIndex> index);

    /// <summary>
    /// Loads sector names from catalogs in the specified <paramref name="catalogsFilePath"/>.
    /// </summary>
    /// <param name="catalogsFilePath">A <see cref="string"/> containing the path to the catalog files.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IMacroNameResourceDataDictionary"/> that is the result.</returns>
    Task<IMacroNameResourceDataDictionary> LoadSectorNamesFromCatalogs(string catalogsFilePath);

    /// <summary>
    /// Loads sector names from entries in the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">An <see cref="IEnumerable{ICatalogIndex}"/> that is the catalog file index.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IMacroNameResourceDataDictionary"/> that is the result.</returns>
    Task<IMacroNameResourceDataDictionary> LoadSectorNamesFromIndex(IEnumerable<ICatalogIndex> index);

    /// <summary>
    /// Loads ship models from catalogs in the specified <paramref name="catalogsFilePath"/>.
    /// </summary>
    /// <param name="catalogsFilePath">A <see cref="string"/> containing the path to the catalog files.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IMacroNameResourceDataDictionary"/> that is the result.</returns>
    Task<IMacroNameResourceDataDictionary> LoadShipModelsFromCatalogs(string catalogsFilePath);

    /// <summary>
    /// Loads ship models from entries in the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">An <see cref="IEnumerable{ICatalogIndex}"/> that is the catalog file index.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IShipModelResourceDataDictionary"/> that is the result.</returns>
    Task<IShipModelResourceDataDictionary> LoadShipModelsFromIndex(IEnumerable<ICatalogIndex> index);

    /// <summary>
    /// Loads text resource data from catalogs in the specified <paramref name="catalogsFilePath"/>.
    /// </summary>
    /// <param name="catalogsFilePath">A <see cref="string"/> containing the path to the catalog files.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="ITextResourcePageDictionary"/> that is the result.</returns>
    Task<ITextResourcePageDictionary> LoadTextResourcesFromCatalogs(string catalogsFilePath);

    /// <summary>
    /// Loads wares from catalogs in the specified <paramref name="catalogsFilePath"/>.
    /// </summary>
    /// <param name="catalogsFilePath">A <see cref="string"/> containing the path to the catalog files.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IWareDataDictionary"/> that is the result.</returns>
    Task<IWareDataDictionary> LoadWaresFromCatalogs(string catalogsFilePath);

    /// <summary>
    /// Loads wares from entries in the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">An <see cref="IEnumerable{ICatalogIndex}"/> that is the catalog file index.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IWareDataDictionary"/> that is the result.</returns>
    Task<IWareDataDictionary> LoadWaresFromIndex(IEnumerable<ICatalogIndex> index);
}
