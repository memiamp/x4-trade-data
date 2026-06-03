namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a trade log trade.
/// </summary>
public interface ITradeLogTradeModel : ITradeModel
{
    /// <summary>
    /// Gets the station the trade was bought from.
    /// </summary>
    IStationModel? BoughtFrom { get; }

    /// <summary>
    /// Gets the station the trade was sold to.
    /// </summary>
    IStationModel? SoldTo { get; }
}
