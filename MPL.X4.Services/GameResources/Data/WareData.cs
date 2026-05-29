namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements a data model of a ware.
/// </summary>
internal class WareData : IWareData
{
    public override string ToString()
        => $"{Id} - Name {NameResource?.ToString()}";

    public required string? ComponentReference { get; init; }

    public required ITextResourceReference? FactoryNameResource { get; init; }

    public required string Group { get; init; }

    public required string Id { get; init; }

    public required ITextResourceReference? NameResource { get; init; }

    public required int PriceAverage { get; init; }

    public required int PriceMaximum { get; init; }

    public required int PriceMinimum { get; init; }

    public required string Transport { get; init; }

    public required int Volume { get; init; }
}
