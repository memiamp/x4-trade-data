namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a trade log trade.
/// </summary>
public interface ITradeLogTradeModel : ITradeModel
{
    /// <summary>
    /// Gets the code of the target the trade was made with.
    /// </summary>
    string TargetCode { get; }

    /// <summary>
    /// Gets the name of the target the trade was made with.
    /// </summary>
    string TargetName { get; }

    /// <summary>
    /// Gets the target type the trade was made with.
    /// </summary>
    TradeTargetType TargetType { get; }
}
