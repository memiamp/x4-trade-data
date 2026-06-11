namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a build storage.
/// </summary>
internal class BuildStorageData : HasTransformBase, IBuildStorageData
{
    public override string ToString()
        => $"{Owner} {Code} - Trades {Trades.Count()} IsKnown {IsKnown} - {Id}";

    public required string? BuildAnchorConnectionId { get; init; }

    public required string? BuildAnchorId { get; init; }

    public required ICargoData Cargo { get; init; }

    public required string Code { get; init; }

    public required string Id { get; init; }

    public required bool IsKnown { get; init; }

    public required string Macro { get; init; }

    public required string Owner { get; init; }

    public required IEnumerable<IShipData> Ships { get; init; }

    public string? State { get; init; }

    public required IEnumerable<ITradeData> Trades { get; init; }
}
