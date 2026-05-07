namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements a data model of a colour resources.
/// </summary>
internal class ColourResourceData : IColourResourceData
{
    public override string ToString()
        => $"Colours: {Colours.Count} Mappings: {Mappings.Count}";

    public required IColourDataDictionary Colours { get; init; }

    public required IMappingDataDictionary Mappings { get; init; }
}
