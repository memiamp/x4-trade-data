namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a gate.
/// </summary>
internal class GateData : HasTransformDataBase, IGateData
{
    public override string ToString()
        => $"{Code} {Macro} - IsKnown {IsKnown} - {Id}";

    public required string Code { get; init; }

    public required string Id { get; init; }

    public required bool IsKnown { get; init; }

    public required string Macro { get; init; }
}
