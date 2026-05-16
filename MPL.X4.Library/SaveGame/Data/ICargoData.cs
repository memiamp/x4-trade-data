namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a cargo.
/// </summary>
public interface ICargoData
{
    /// <summary>
    /// Gets the ware items in the cargo.
    /// </summary>
    IEnumerable<IWareItemData> Items { get; }
}
