namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements a data model of a sector name.
/// </summary>
internal class SectorNameData : ISectorNameData
{
    public override string ToString()
        => $"{Id} - {NameResource}";

    public required string Id { get; init; }

    public required ITextResourceReference NameResource { get; init; }
}
