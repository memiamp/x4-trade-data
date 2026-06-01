namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a lockbox.
/// </summary>
public interface ILockboxModel : IHasIsKnown, IHasTransform, IModelWithId
{
    /// <summary>
    /// Gets the code of the lockbox.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the number of locks on the lockbox.
    /// </summary>
    int LockCount { get; }

    /// <summary>
    /// Gets the drop rarity of the lockbox.
    /// </summary>
    LockboxRarity Rarity { get; }

    /// <summary>
    /// Gets explicit wares in the lockbox.
    /// </summary>
    IEnumerable<string> Wares { get; }
}
