namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a cargo.
/// </summary>
internal class CargoData : ICargoData
{
    public override string ToString()
        => $"Cargo count: {Items.Count()}";

    public required IEnumerable<ICargoItemData> Items { get; init; }
}
