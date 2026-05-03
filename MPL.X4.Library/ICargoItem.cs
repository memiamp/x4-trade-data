namespace MPL.X4;

/// <summary>
/// An interface that defines an item of cargo.
/// </summary>
public interface ICargoItem
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
