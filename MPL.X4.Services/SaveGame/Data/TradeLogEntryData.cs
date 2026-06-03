using System.Collections;

namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a trade log entry.
/// </summary>
internal class TradeLogEntryData : LogEntryData, ITradeLogEntryData
{
    public override string ToString()
        => $"{Ware} {Volume} {Price}";

    public required string BuyerId { get; init; }

    public required int Price { get; init; }

    public required string SellerId { get; init; }

    public required int Volume { get; init; }

    public required string Ware { get; init; }
}
