namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a collectable drop.
/// </summary>
/// <typeparam name="TDrop">The type of the item in the drop.</typeparam>
internal class CollectableDropData<TDrop> : ICollectableDropData<TDrop>
{
    public override string ToString()
        => $"{Macro} Drop Items: {Items.Count()}";

    public required bool IsKnown { get; init; }

    public required IEnumerable<TDrop> Items { get; init; }

    public required string Macro { get; init; }

    public required string? State { get; init; }

    public required ITransform3D Transform { get; init; }
}
