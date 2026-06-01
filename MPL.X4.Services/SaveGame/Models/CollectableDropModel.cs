using MPL.X4.GameResources.Models;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a collectable drop.
/// </summary>
internal class CollectableDropModel : ModelWithIdBase, ICollectableDropModel
{
    public override string ToString()
        => $"{Name} {Amount} - {Id}";

    public required int Amount { get; init; }

    public required bool IsKnown { get; init; }

    public required bool IsWreck { get; init; }

    public required string Name { get; init; }

    public required ITransform3D Transform { get; init; }

    public required DropType Type { get; init; }

    public required WareType WareType { get; init; }
}
