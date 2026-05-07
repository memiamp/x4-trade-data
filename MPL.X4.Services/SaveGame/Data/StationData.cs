namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a station.
/// </summary>
internal class StationData : IStationData
{
    public override string ToString()
        => $"{Owner} {NameId} {Code} - Trades {Trades.Count()} IsKnown {IsKnown} - {Id}";

    public required string Code { get; init; }

    public required string Id { get; init; }

    public required bool IsKnown { get; init; }

    public required ITextResourceReference? NameId { get; init; }

    public required string Owner { get; init; }

    public required IPosition3D Position { get; init; }

    public required IEnumerable<ITradeData> Trades { get; init; }
}
