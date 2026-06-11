namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a station.
/// </summary>
internal class StationModel : StationModelBase, IStationModel
{
    public override string ToString()
        => $"{Name} {Owner} - IsKnown {IsKnown} - {Id}";

    public required IBuildStorageModel? BuildStorage { get; init; }

    public required bool IsAbandoned { get; init; }

    public required bool IsUnderConstruction { get; init; }

    public required string Name { get; init; }

    public required IProductionModelList Productions { get; init; }
}
