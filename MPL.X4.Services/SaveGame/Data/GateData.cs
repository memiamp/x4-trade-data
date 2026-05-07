namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a gate.
/// </summary>
internal class GateData : IGateData
{
    public override string ToString()
        => $"{Code} - IsKnown {IsKnown} - {Id}";

    public required string Code { get; set; }

    public required string Id { get; set; }

    public required bool IsKnown { get; set; }

    public required IPosition3D Position { get; set; }
}
