namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a faction model.
/// </summary>
internal class FactionModel : ModelWithIdBase, IFactionModel
{
    /// <summary>
    /// Gets the default model.
    /// </summary>
    /// <returns>An <see cref="IFactionModel"/> that is the result.</returns>
    internal static IFactionModel GetDefault()
        => new FactionModel
        {
            Acronym = string.Empty,
            Colour = ColourModel.GetDefault(),
            Id = string.Empty,
            Name = string.Empty
        };

    public override string ToString()
        => $"{Name} ({Acronym}) - {Id}";

    public required string Acronym { get; init; }

    public required IColourModel Colour { get; init; }

    public required string Name { get; init; }
}
