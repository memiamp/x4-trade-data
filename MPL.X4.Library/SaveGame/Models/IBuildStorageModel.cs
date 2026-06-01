namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a build storage.
/// </summary>
public interface IBuildStorageModel : IHasIsKnown, IHasOwner, IHasTransform, IModelWithId
{
    /// <summary>
    /// Gets the build storage cargo.
    /// </summary>
    ICargoItemModelList Cargo { get; }

    /// <summary>
    /// Gets the code of the build storage.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets an indication of whether the build storage is a wreck.
    /// </summary>
    bool IsWreck { get; }

    /// <summary>
    /// Gets the trades on offer at the build storage.
    /// </summary>
    ITradeModelList Trades { get; }
}
