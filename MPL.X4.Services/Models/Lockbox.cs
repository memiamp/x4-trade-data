namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a lockbox.
/// </summary>
internal class Lockbox : ILockbox
{
    public override string ToString()
        => $"{Type} {Code} - IsKnown {IsKnown} - {Id}";

    public required string Code { get; init; }

    public required string Id { get; init; }

    public required bool IsKnown { get; init; }

    public required ISectorPosition Position { get; init; }

    public required string Type { get; init; }
}
