namespace MPL.X4.TradeData;

/// <summary>
/// An interface that defines resource data.
/// </summary>
public interface IResourceData
{
    /// <summary>
    /// Gets sector name resource data.
    /// </summary>
    Dictionary<int, string> SectorNames { get; }
}
