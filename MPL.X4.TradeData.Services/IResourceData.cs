using MPL.X4.Data;

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
    /// Gets a sector name from the specified <paramref name="macro"/>.
    /// </summary>
    /// <param name="macro">A <see cref="string"/> containing the macro to lookup.</param>
    /// <returns>A <see cref="string"/> containing the result.</returns>
    string LookupSectorNameFromMacro(string macro);

    /// <summary>
    /// Gets colour map data.
    /// </summary>
    Dictionary<string, IColour> ColourMap { get; }

    /// <summary>
    /// Gets faction resource data.
    /// </summary>
    IFactionsData Factions { get; }

    /// <summary>
    /// Gets landmark resource data.
    /// </summary>
    Dictionary<int, string> Landmarks { get; }

    /// <summary>
    /// Gets sector macro name map.
    /// </summary>
    Dictionary<string, string> SectorMacros { get; }

    /// <summary>
    /// Gets sector name resource data.
    /// </summary>
    Dictionary<int, string> SectorNames { get; }

    /// <summary>
    /// Gets station name resource data.
    /// </summary>
    Dictionary<int, string> StationNames { get; }

    /// <summary>
    /// Gets zone offset resource data.
    /// </summary>
    Dictionary<string, IZoneOffset> ZoneOffsets { get; }
}
