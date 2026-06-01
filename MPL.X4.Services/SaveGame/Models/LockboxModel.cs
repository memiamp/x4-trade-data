namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a lockbox.
/// </summary>
internal class LockboxModel : ModelWithIdBase, ILockboxModel
{
    public override string ToString()
        => $"{Rarity} {LockCount} - IsKnown {IsKnown} - {Id}";

    public required string Code { get; init; }

    public required bool IsKnown { get; init; }

    public required int LockCount { get; init; }

    public required LockboxRarity Rarity { get; init; }

    public required ITransform3D Transform { get; init; }

    public required IEnumerable<string> Wares { get; init; }
}
