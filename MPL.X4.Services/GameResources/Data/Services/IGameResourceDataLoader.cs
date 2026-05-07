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
    /// <param name="index">An <see cref="IEnumerable{ICatalogIndexEntry}"/> that is the catalog file index.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IColourResourceData"/> that is the result.</returns>
    Task<IColourResourceData> LoadColourResourcesFromIndex(IEnumerable<ICatalogIndexEntry> index);

    /// <summary>
    /// Loads faction data from catalogs in the specified <paramref name="catalogsFilePath"/>.
    /// </summary>
    /// <param name="catalogsFilePath">A <see cref="string"/> containing the path to the catalog files.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IFactionDataDictionary"/> that is the result.</returns>
    Task<IFactionDataDictionary> LoadFactionsFromCatalogs(string catalogsFilePath);

    /// <summary>
    /// Loads faction data from entries in the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">An <see cref="IEnumerable{ICatalogIndexEntry}"/> that is the catalog file index.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IFactionDataDictionary"/> that is the result.</returns>
    Task<IFactionDataDictionary> LoadFactionsFromIndex(IEnumerable<ICatalogIndexEntry> index);

    /// <summary>
    /// Loads a map of macro to sector name mapping from catalogs in the specified <paramref name="catalogsFilePath"/>.
    /// </summary>
    /// <param name="catalogsFilePath">A <see cref="string"/> containing the path to the catalog files.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="Dictionary{TKey, TValue}"/> that is the result.</returns>
    Task<Dictionary<string, string>> LoadSectorNameMacroMapFromCatalogs(string catalogsFilePath);

    /// <summary>
    /// Loads a map of macro to sector name mapping from entries in the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">An <see cref="IEnumerable{ICatalogIndexEntry}"/> that is the catalog file index.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="Dictionary{TKey, TValue}"/> that is the result.</returns>
    Task<Dictionary<string, string>> LoadSectorNameMacroMapFromIndex(IEnumerable<ICatalogIndexEntry> index);

    /// <summary>
    /// Loads text resource data from catalogs in the specified <paramref name="catalogsFilePath"/>.
    /// </summary>
    /// <param name="catalogsFilePath">A <see cref="string"/> containing the path to the catalog files.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="ITextResourcePageDictionary"/> that is the result.</returns>
    Task<ITextResourcePageDictionary> LoadTextResourcesFromCatalogs(string catalogsFilePath);

    /// <summary>
    /// Loads zone offset data from catalogs in the specified <paramref name="catalogsFilePath"/>.
    /// </summary>
    /// <param name="catalogsFilePath">A <see cref="string"/> containing the path to the catalog files.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IZoneOffsetDataDictionary"/> that is the result.</returns>
    Task<IZoneOffsetDataDictionary> LoadZoneOffsetsFromCatalogs(string catalogsFilePath);

    /// <summary>
    /// Loads zone offset data from entries in the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">An <see cref="IEnumerable{ICatalogIndexEntry}"/> that is the catalog file index.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  An <see cref="IZoneOffsetDataDictionary"/> that is the result.</returns>
    Task<IZoneOffsetDataDictionary> LoadZoneOffsetsFromIndex(IEnumerable<ICatalogIndexEntry> index);
}
