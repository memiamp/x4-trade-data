namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a lockbox.
/// </summary>
public interface ILockboxData : IHasTransform
{
    /// <summary>
    /// Gets the code of the lockbox.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the identifier of the lockbox.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets an indication of whether the lockbox is known to the player.
    /// </summary>
    bool IsKnown { get; }

    /// <summary>
    /// Gets the number of locks on the lockbox.
    /// </summary>
    int LockCount { get; }

    /// <summary>
    /// Gets the lockbox macro.
    /// </summary>
    string Macro { get; }

    /// <summary>
    /// Gets explicit wares in the lockbox.
    /// </summary>
    IEnumerable<IWareItemData> Wares { get; }
}
