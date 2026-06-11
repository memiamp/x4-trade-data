namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a removed object.
/// </summary>
internal class RemovedObjectData : IRemovedObjectData
{
    public override string ToString()
        => $"{Id} {Name} {Code}";
    
    public required string Code { get; init; }

    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string Owner { get; init; }

    public required string Space { get; init; }
}
