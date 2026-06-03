namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a trade log ship.
/// </summary>
public interface ITradeLogShipModel : IModelWithId
{
    /// <summary>
    /// Gets the code of the ship.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the name of the ship.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the trades made by this ship.
    /// </summary>
    ITradeLogTradeModelList Trades { get; }
}
