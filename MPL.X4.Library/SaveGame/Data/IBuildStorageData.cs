namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a build storage.
/// </summary>
public interface IBuildStorageData : IHasIsKnown, IHasState, IHasTransform
{
    /// <summary>
    /// Gets the build anchor connection identifier for this build storage.
    /// </summary>
    string? BuildAnchorConnectionId { get; }

    /// <summary>
    /// Gets the build anchor identifier for this build storage.
    /// </summary>
    string? BuildAnchorId { get; }

    /// <summary>
    /// Gets the build storage cargo.
    /// </summary>
    ICargoData Cargo { get; }

    /// <summary>
    /// Gets the code of the build storage.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the identifier of the build storage.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the macro of the build storage.
    /// </summary>
    string Macro { get; }

    /// <summary>
    /// Gets the owner of the build storage.
    /// </summary>
    string Owner { get; }

    /// <summary>
    /// Gets the ships docked at the build storage.
    /// </summary>
    IEnumerable<IShipData> Ships { get; }

    /// <summary>
    /// Gets the trades on offer at the build storage.
    /// </summary>
    IEnumerable<ITradeData> Trades { get; }
}
