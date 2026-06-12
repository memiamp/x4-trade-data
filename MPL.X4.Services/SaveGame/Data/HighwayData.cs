namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model for a highway.
/// </summary>
internal class HighwayData : HasTransformBase, IHighwayData
{
    public override string ToString()
        => $"{Code} {Transform} - Ships {Ships.Count()} - {Id}";

    public required string Code { get; init; }

    public required string Id { get; init; }

    public required bool IsKnown { get; init; }
  
    public required string Macro { get; init; }

    public required IEnumerable<IShipData> Ships { get; init; }
}
