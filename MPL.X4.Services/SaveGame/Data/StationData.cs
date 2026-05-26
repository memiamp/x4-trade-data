namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a station.
/// </summary>
internal class StationData : HasTransformBase, IStationData
{
    public override string ToString()
        => $"{Owner} {Name ?? NameResource?.ToString() ?? BaseNameResource?.ToString() ?? "Unknown"} {Code} - Trades {Trades.Count()} IsKnown {IsKnown} - {Id}";

    public required ITextResourceReference? BaseNameResource { get; init; }

    public required string? BuildingModuleConnectionId { get; init; }

    public required string? BuildingModuleId { get; init; }

    public required string Code { get; init; }

    public required int DefenceModuleCount { get; init; }

    public required string Id { get; init; }

    public required bool IsKnown { get; init; }

    public required string Macro { get; init; }

    public required string? Name { get; init; }

    public required int NameIndex { get; init; }

    public required ITextResourceReference? NameResource { get; init; }

    public required string Owner { get; init; }

    public required IEnumerable<string> Productions { get; init; }

    public string? State { get; init; }

    public required IEnumerable<ITradeData> Trades { get; init; }
}
