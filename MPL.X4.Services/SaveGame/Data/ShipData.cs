namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a ship.
/// </summary>
internal class ShipData : IShipData
{
    public override string ToString()
        => $"{Owner} {Class} {Macro} {Code} - {Position} - IsKnown {IsKnown} - {Id}";

    public required ICargoData Cargo { get; set; }

    public required string Class { get; init; }

    public required string Code { get; init; }

    public required string Id { get; init; }

    public required bool IsKnown { get; init; }

    public required string Macro { get; init; }

    public required IEnumerable<string> Modifications { get; init; }

    public required string Owner { get; init; }

    public required IPosition3D Position { get; init; }
}
