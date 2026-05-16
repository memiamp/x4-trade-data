namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of an item of ware.
/// </summary>
public interface IWareItemData
{
    /// <summary>
    /// Gets the amount.
    /// </summary>
    int Amount { get; }

    /// <summary>
    /// Gets the buy price.
    /// </summary>
    int Buy { get; }

    /// <summary>
    /// Gets the ware price.
    /// </summary>
    int Price { get; }

    /// <summary>
    /// Gets the sell price.
    /// </summary>
    int Sell { get; }

    /// <summary>
    /// Gets the ware.
    /// </summary>
    string Ware { get; }
}
