namespace MPL.X4.TradeData.Services;

/// <summary>
/// An interface that defines the behaviour of a resource file reader.
/// </summary>
public interface IResourceData
{
    /// <summary>
    /// Looks up the specified resource.
    /// </summary>
    /// <param name="resource">An <see cref="ITextResourceReference"/> that is to be looked up.</param>
    /// <returns>A <see cref="string"/> containing the result.</returns>
    string Lookup(int pageId, int textId);

    /// <summary>
    /// Looks up the specified resource.
    /// </summary>
    /// <param name="resource">An <see cref="ITextResourceReference"/> that is to be looked up.</param>
    /// <returns>A <see cref="string"/> containing the result.</returns>
    string Lookup(ITextResourceReference resource);

    /// <summary>
    /// Gets landmark resource data.
    /// </summary>
    Dictionary<int, string> Landmarks { get; }

    /// <summary>
    /// Gets sector name resource data.
    /// </summary>
    Dictionary<int, string> SectorNames { get; }

    /// <summary>
    /// Gets station name resource data.
    /// </summary>
    Dictionary<int, string> StationNames { get; }
}
