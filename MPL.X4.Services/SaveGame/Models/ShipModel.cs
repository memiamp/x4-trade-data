namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a ship.
/// </summary>
internal class ShipModel : ModelWithIdBase, IShipModel
{
    public override string ToString()
        => $"{Class} {Model} - IsKnown {IsKnown} - {Id}";

    public required ShipClass Class { get; set; }

    public required string Code { get; init; }

    public required bool IsKnown { get; init; }

    public required string Model { get; set; }

    public required string? Name { get; set; }

    public required string Owner { get; set; }

    public required ITransform3D Transform { get; init; }
}
