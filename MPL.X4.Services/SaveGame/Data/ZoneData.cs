namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a zone.
/// </summary>
internal class Zone : IZone
{
    public override string ToString()
        => $"{Code} {Position} - Gates {Gates.Count()} Lockboxes {Lockboxes.Count()} Ships {Ships.Count()} Stations {Stations.Count()} - {Id}";

    public required string Code { get; init; }

    public required IEnumerable<IGate> Gates { get; init; }

    public required string Id { get; init; }

    public required bool IsKnown { get; init; }
  
    public required IEnumerable<ILockbox> Lockboxes { get; init; }

    public required ISectorPosition Position { get; init; }

    public required IEnumerable<IShip> Ships { get; init; }

    public required IEnumerable<IStation> Stations { get; init; }
}
