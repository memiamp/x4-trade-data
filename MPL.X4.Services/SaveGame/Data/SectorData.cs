namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a sector.
/// </summary>
internal class Sector : ISector
{
    public override string ToString()
        => $"{Owner} {Macro} {Code} - Stations {Stations.Count()} Ships {Ships.Count()} IsKnown {IsKnown} - {Id}";

    public required string Code { get; init; }

    public required IEnumerable<IGate> Gates { get; init; }
 
    public required string Id { get; init; }

    public required bool IsKnown { get; init; }

    public required IEnumerable<ILockbox> Lockboxes { get; init; }

    public required string Macro { get; init; }

    public required string Owner { get; init; }

    public required IEnumerable<IShip> Ships { get; init; }

    public required IEnumerable<IStation> Stations { get; init; }
}
