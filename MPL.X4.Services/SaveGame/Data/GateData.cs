namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a gate.
/// </summary>
internal class Gate : IGate
{
    public override string ToString()
        => $"{Code} - IsKnown {IsKnown} - {Id}";

    public required string Code { get; set; }

    public required string Id { get; set; }

    public required bool IsKnown { get; set; }

    public required ISectorPosition Position { get; set; }
}
