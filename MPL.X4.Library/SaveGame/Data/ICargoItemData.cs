namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of an item of cargo.
/// </summary>
public interface ICargoItemData
{
    /// <summary>
    /// Gets the amount.
    /// </summary>
    int Amount { get; }

    /// <summary>
    /// Gets the ware.
    /// </summary>
    string Ware { get; }
}
