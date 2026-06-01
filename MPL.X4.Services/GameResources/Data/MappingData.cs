namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements a data model of a mapping.
/// </summary>
internal class MappingData : IMappingData
{
    public override string ToString()
        => $"{Id} - {Reference}";

    public required string Id { get; init; }

    public required string Reference { get; init; }
}
