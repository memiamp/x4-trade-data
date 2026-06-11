namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a trade log entry.
/// </summary>
internal class TradeLogEntryModel : TradeModel, ITradeLogEntryModel
{
    public override string ToString()
        => $"{TradeAge} - {Buyer} {Seller} - {Name} {Amount}";

    public required ITradePartnerModel Buyer { get; init; }

    public required ITradePartnerModel Seller { get; init; }

    public required double Time { get; init; }

    public required TimeSpan TradeAge { get; init; }
}
