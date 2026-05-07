namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a lockbox.
/// </summary>
internal class Lockbox : ILockbox
{
    public override string ToString()
        => $"{Type} {Code} - IsKnown {IsKnown} - {Id}";

    public required string Code { get; set; }

    public required string Id { get; set; }

    public required bool IsKnown { get; set; }

    public required int LockCount { get; set; }

    public required ISectorPosition Position { get; set; }

    public required string Type { get; set; }

    public required IEnumerable<string> Wares { get; init; }
}
