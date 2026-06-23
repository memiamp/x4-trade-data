namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements game resource data.
/// </summary>
internal class GameResourceData : IGameResourceData
{
    public required IColourResourceData Colours { get; init; }

    public required IFactionDataDictionary Factions { get; init; }

    public required IMacroNameResourceDataDictionary LandmarkNames { get; init; }

    public required IOffsetDataDictionary Offsets { get; init; }

    public required IMacroNameResourceDataDictionary SectorNames { get; init; }

    public required IShipModelResourceDataDictionary ShipModels { get; init; }

    public required ITextResourcePageDictionary Text { get; init; }

    public required IWareDataDictionary Wares { get; init; }
}
