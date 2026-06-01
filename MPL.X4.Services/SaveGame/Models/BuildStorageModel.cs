using MPL.X4.GameResources.Models;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a build storage.
/// </summary>
internal class BuildStorageModel : ModelWithIdBase, IBuildStorageModel
{
    public override string ToString()
        => $"{Owner} - IsKnown {IsKnown} - {Id}";

    public required ICargoItemModelList Cargo { get; init; }

    public required string Code { get; init; }

    public required bool IsKnown { get; init; }

    public required bool IsWreck { get; init; }

    public required IFactionModel Owner { get; init; }

    public required ITradeModelList Trades { get; init; }

    public required ITransform3D Transform { get; init; }
}
