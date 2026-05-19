namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a list of trade models.
/// </summary>
public interface ITradeModelList : IModelWithIdList<ITradeModel>
{
    /// <summary>
    /// Gets all buy trades.
    /// </summary>
    IEnumerable<ITradeModel> Buys => this.Where(x => x.Type == TradeType.Buy);

    /// <summary>
    /// Gets all sell trades.
    /// </summary>
    IEnumerable<ITradeModel> Sells => this.Where(x => x.Type == TradeType.Sell);
}
