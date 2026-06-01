namespace MPL.X4.Catalog;

/// <summary>
/// An interface that defines a catalog index file.
/// </summary>
public interface ICatalogIndex
{
    /// <summary>
    /// Gets the path of the data file for this index.
    /// </summary>
    string DataFilePath { get; }

    /// <summary>
    /// Gets any dependencies for this index file.
    /// </summary>
    IEnumerable<string> Dependencies { get; }

    /// <summary>
    /// Gets the entries in this index file.
    /// </summary>
    IEnumerable<ICatalogIndexEntry> Entries { get; }

    /// <summary>
    /// Gets the game pack this index belongs to.
    /// </summary>
    string GamePack { get; }

    /// <summary>
    /// Gets the path of the index file.
    /// </summary>
    string IndexFilePath { get; }

    /// <summary>
    /// Gets an indication of whether this catalog index belongs to the base game.
    /// </summary>
    bool IsBaseGameData => GamePack == Constants.CatalogFile.BaseGame;

    /// <summary>
    /// Gets an indication of whether this catalog index belongs to a modification.
    /// </summary>
    bool IsGameMod => GamePack == Constants.CatalogFile.GameExtension;
}
