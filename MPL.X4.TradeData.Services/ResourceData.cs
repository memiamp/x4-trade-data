using MPL.X4.Data;

namespace MPL.X4.TradeData.Services;

/// <summary>
/// A class that implements resource data.
/// </summary>
/// <param name="rawResources">A <see cref="Dictionary{TKey, TValue}"/> containing the raw resource data.</param>
internal class ResourceData(
                            Dictionary<int, Dictionary<int, string>> rawResources)
    : IResourceData
{
    string IResourceData.Lookup(int pageId, int textId)
    {
        if (rawResources.TryGetValue(pageId, out var data))
        {
            if (data.TryGetValue(textId, out var returnValue))
            {
                return returnValue;
            }

            throw new ArgumentException("The specified resource text not be found", nameof(textId));
        }

        throw new ArgumentException("The specified resource page could not be found", nameof(pageId));
    }

    string IResourceData.Lookup(ITextResourceReference resource)
        => ((IResourceData)this).Lookup(resource.PageId, resource.TextId);

    string IResourceData.LookupSectorNameFromMacro(string macro)
        => SectorMacros.TryGetValue(macro, out var returnValue)
                                                                ? returnValue
                                                                : macro;

    public required Dictionary<string, IColour> ColourMap { get; init; }

    public required IFactionsData Factions { get; init; }

    public required Dictionary<int, string> Landmarks { get; init; }

    public required Dictionary<string, string> SectorMacros { get; init; }

    public required Dictionary<int, string> SectorNames { get; init; }

    public required Dictionary<int, string> StationNames { get; init; }

    public required Dictionary<string, IZoneOffset> ZoneOffsets { get; init; }
}
