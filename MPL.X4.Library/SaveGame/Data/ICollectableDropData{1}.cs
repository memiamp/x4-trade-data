namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a collectable drop.
/// </summary>
public interface ICollectableDropData<TDrop> : IHasIsKnown, IHasState, IHasTransform
{
    /// <summary>
    /// Gets the items for the collectable drop.
    /// </summary>
    IEnumerable<TDrop> Items { get; }

    /// <summary>
    /// Gets the macro of the collectable drop.
    /// </summary>
    string Macro { get; }
}
