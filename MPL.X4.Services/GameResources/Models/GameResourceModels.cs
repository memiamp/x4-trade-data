namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements game resource models.
/// </summary>
internal class GameResourceModels : IGameResourceModels
{
    public required IColourModelList Colours { get; init; }

    public required IFactionModelList Factions { get; init; }

    //public required Dictionary<string, string> SectorMacros { get; init; }

    //public required ITextResourcePageDictionary Text { get; init; }

    //public required IZoneOffsetDataDictionary ZoneOffsets { get; init; }
}
