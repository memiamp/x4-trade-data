using MPL.X4.GameResources.Models;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a ship.
/// </summary>
internal class ShipModel : ModelWithIdBase, IShipModel
{
    public override string ToString()
        => $"{Class} {Model} - IsKnown {IsKnown} - {Id}";

    public required ICargoItemModelList Cargo { get; init; }

    public required ShipClass Class { get; init; }

    public required string Code { get; init; }

    public required bool IsAbandoned { get; init; }

    public required bool IsKnown { get; init; }

    public required bool IsWreck { get; init; }

    public required string Model { get; init; }

    public required IModificationModelList Modifications { get; init; }

    public required string? Name { get; init; }

    public required IFactionModel Owner { get; init; }

    public required ITransform3D Transform { get; init; }

}
