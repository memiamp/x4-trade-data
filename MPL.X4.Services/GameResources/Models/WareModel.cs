namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a ware model.
/// </summary>
internal class WareModel : ModelWithIdBase, IWareModel
{
    public override string ToString()
        => $"{Name} ({Type}) - {Id}";

    public required string? ComponentReference { get; init; }

    public required string? FactoryName { get; init; }

    public required string Group { get; init; }

    public required string Name { get; init; }

    public required int PriceAverage { get; init; }

    public required int PriceMaximum { get; init; }

    public required int PriceMinimum { get; init; }

    public required string Transport { get; init; }

    public required WareType Type { get; init; }

    public required int Volume { get; init; }
}
