namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements a data model of a faction.
/// </summary>
internal class FactionData : IFactionData
{
    public override string ToString()
        => $"{Id} - Name {NameResource?.ToString()} Acronym {AcronymResource?.ToString()} ColourReference {ColourReference}";

    public ITextResourceReference? AcronymResource { get; set; }

    public string? ColourReference { get; set; }

    public required string Id { get; set; }

    public ITextResourceReference? NameResource { get; set; }
}
