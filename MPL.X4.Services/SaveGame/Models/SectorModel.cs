namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a sector.
/// </summary>
internal class SectorModel : SectorModelBase, ISectorModel
{
    public override string ToString()
        => $"{Name} {Owner} {Code} - IsKnown {IsKnown} - {Id}";

    public required IBuildStorageModelList BuildStorages { get; init; }

    public required ICollectableDropModelList CollectableDrops { get; init; }

    public required IGateModelList Gates { get; init; }

    public required ILockboxModelList Lockboxes { get; init; }

    public required string Name { get; init; }

    public required IStationModelList Stations { get; init; }
}
