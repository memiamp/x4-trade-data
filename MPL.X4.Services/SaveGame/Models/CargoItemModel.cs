namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a cargo item.
/// </summary>
internal class CargoItemModel : ModelWithIdBase, ICargoItemModel
{
    public override string ToString()
        => $"{Name} {Amount} - {Id}";

    public required int Amount { get; init; }

    public required string Name { get; init; }
}
