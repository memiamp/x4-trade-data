namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a ship.
/// </summary>
internal class ShipModel : SectorElementWithCargoModelBase, IShipModel
{
    public override string ToString()
        => $"{Class} {Model} - IsKnown {IsKnown} - {Id}";

    public required ShipClass Class { get; init; }

    public required IEngineModificationModel? EngineModification { get; init; }

    public required bool IsAbandoned { get; init; }

    public required string Model { get; init; }

    public required string? Name { get; init; }

    public required IPaintModificationModel? PaintModification { get; init; }

    public required IShieldModificationModel? ShieldModification { get; init; }

    public required IShipModificationModel? ShipModification { get; init; }

    public required IEnumerable<IWeaponModificationModel> WeaponModifications { get; init; }
}
