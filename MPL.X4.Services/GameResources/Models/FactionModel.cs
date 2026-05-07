namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a faction model.
/// </summary>
internal class FactionModel : IFactionModel
{
    public override string ToString()
        => $"{Name} ({Acronym}) - {Id}";

    public required string Acronym { get; init; }

    public required IColourModel Colour { get; init; }

    public required string Id { get; init; }

    public required string Name { get; init; }
}
