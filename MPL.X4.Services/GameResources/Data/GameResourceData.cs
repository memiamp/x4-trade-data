namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements game resource data.
/// </summary>
internal class GameResourceData : IGameResourceData
{
    public required IColourResourceData Colours { get; init; }

    public required IFactionDataDictionary Factions { get; init; }

    public required Dictionary<string, string> SectorMacros { get; init; }

    public required ITextResourcePageDictionary Text { get; init; }

    public required IZoneOffsetDataDictionary ZoneOffsets { get; init; }
}
