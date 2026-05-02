namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a faction.
/// </summary>
internal class Faction : IFaction
{
    public override string ToString()
        => $"{Id} - Name {Name ?? NameResource?.ToString()} Acronym {Acronym ?? AcronymResource?.ToString()} ColourRef {ColourRef}";

    public string? Acronym { get; set; }

    public ITextResourceReference? AcronymResource { get; set; }

    public string? ColourRef { get; set; }

    public required string Id { get; set; }

    public string? Name { get; set; }

    public ITextResourceReference? NameResource { get; set; }
}
