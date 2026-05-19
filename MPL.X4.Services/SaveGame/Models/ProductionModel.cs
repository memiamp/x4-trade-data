namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a production.
/// </summary>
internal class ProductionModel : ModelWithIdBase, IProductionModel
{
    public override string ToString()
        => $"{Name} {ModuleCount} - {Id}";

    public required int ModuleCount { get; init; }

    public required string Name { get; init; }
}
