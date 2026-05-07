namespace MPL.X4;

/// <summary>
/// An interface that defines a cargo.
/// </summary>
public interface ICargo
{
    /// <summary>
    /// Gets the items in the cargo.
    /// </summary>
    IEnumerable<ICargoItem> Items { get; }
}
