using MPL.X4.GameResources.Models;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a station.
/// </summary>
internal class StationModel : ModelWithIdBase, IStationModel
{
    public override string ToString()
        => $"{Name} {Owner} - IsKnown {IsKnown} - {Id}";

    public required IBuildStorageModel? BuildStorage { get; init; }

    public required string Code { get; init; }

    public required bool IsAbandoned { get; init; }

    public required bool IsKnown { get; init; }

    public required bool IsUnderConstruction { get; init; }

    public required bool IsWreck { get; init; }

    public required string Name { get; init; }

    public required IFactionModel Owner { get; init; }

    public required ITradeModelList Trades { get; init; }

    public required IProductionModelList Productions { get; init; }

    public required ITransform3D Transform { get; init; }
}
