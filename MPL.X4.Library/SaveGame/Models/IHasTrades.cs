namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines an element that has trades.
/// </summary>
public interface IHasTrades
{
    /// <summary>
    /// Gets the trades on offer.
    /// </summary>
    ITradeModelList Trades { get; }
}
