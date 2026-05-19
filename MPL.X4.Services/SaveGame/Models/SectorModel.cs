using MPL.X4.GameResources.Models;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a sector.
/// </summary>
internal class SectorModel : ModelWithIdBase, ISectorModel
{
    public override string ToString()
        => $"{Name} {Owner} {Code} - IsKnown {IsKnown} - {Id}";

    public required string Code { get; init; }

    public required IGateModelList Gates { get; init; }

    public required bool IsKnown { get; init; }

    public required ILockboxModelList Lockboxes { get; init; }

    public required string Name { get; init; }

    public required IFactionModel Owner { get; init; }

    public required IShipModelList Ships { get; init; }

    public required IStationModelList Stations { get; init; }

    public required ITransform3D Transform { get; init; }
}
