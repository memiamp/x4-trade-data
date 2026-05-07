namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a lockbox.
/// </summary>
internal class LockboxData : ILockboxData
{
    public override string ToString()
        => $"{Type} {Code} - IsKnown {IsKnown} - {Id}";

    public required string Code { get; set; }

    public required string Id { get; set; }

    public required bool IsKnown { get; set; }

    public required int LockCount { get; set; }

    public required IPosition3D Position { get; set; }

    public required string Type { get; set; }

    public required IEnumerable<string> Wares { get; init; }
}
