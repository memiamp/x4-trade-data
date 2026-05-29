using MPL.X4.SaveGame.Models;
using MPL.X4.TradeData.UI.Models.SpecialItem;

namespace MPL.X4.TradeData.UI.Services;

/// <summary>
/// An interface that defines the behaviour of a provider of special item data.
/// </summary>
internal interface ISpecialItemDataProvider
{
    /// <summary>
    /// Gets abandoned build storages.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{ISpecialItem}"/> containing the results.</returns>
    IEnumerable<ISpecialItem> GetAbandonedBuildStorages();

    /// <summary>
    /// Gets abandoned ships.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{ISpecialItem}"/> containing the results.</returns>
    IEnumerable<ISpecialItem> GetAbandonedShips();

    /// <summary>
    /// Gets lockboxes.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{ISpecialItem}"/> containing the results.</returns>
    IEnumerable<ISpecialItem> GetLockboxes();

    /// <summary>
    /// Gets the top <paramref name="count"/> build storages.
    /// </summary>
    /// <param name="count">An <see cref="int"/> indicating the number of build storages to return.</param>
    /// <returns>An <see cref="IEnumerable{ISpecialItem}"/> containing the results.</returns>
    IEnumerable<ISpecialItem> GetTopBuildStorages(int count);

    /// <summary>
    /// Gets an indication of whether there is a save game.
    /// </summary>
    bool HasSaveGame => SaveGame is not null;

    /// <summary>
    /// Gets or sets the save game data.
    /// </summary>
    ISaveGameModels? SaveGame { get; set; }
}
