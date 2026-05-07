namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a station.
/// </summary>
internal class Station : IStation
{
    public override string ToString()
        => $"{Owner} {NameId} {Code} - Trades {Trades.Count()} IsKnown {IsKnown} - {Id}";

    public required string Code { get; init; }

    public required string Id { get; init; }

    public required bool IsKnown { get; init; }

    public required ITextResourceReference? NameId { get; init; }

    public required string Owner { get; init; }

    public required ISectorPosition Position { get; init; }

    public required IEnumerable<ITrade> Trades { get; init; }
}
