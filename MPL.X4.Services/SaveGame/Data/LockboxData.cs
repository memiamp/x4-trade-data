namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a lockbox.
/// </summary>
internal class LockboxData : HasTransformDataBase, ILockboxData
{
    public override string ToString()
        => $"{Type} {Code} - IsKnown {IsKnown} - {Id}";

    public required string Code { get; init; }

    public required string Id { get; init; }

    public required bool IsKnown { get; init; }

    public required int LockCount { get; init; }

    public required string Type { get; init; }

    public required IEnumerable<string> Wares { get; init; }
}
