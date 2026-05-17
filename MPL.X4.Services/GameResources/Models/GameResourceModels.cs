namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements game resource models.
/// </summary>
internal class GameResourceModels : IGameResourceModels
{
    public required IColourModelList Colours { get; init; }

    public required IFactionModelList Factions { get; init; }

    public required IOffsetModelReference Offsets { get; init; }

    public required IMacroNameModelList SectorNames { get; init; }

    public required IMacroNameModelList ShipModels { get; init; }

    public required ITextResourceModelList Text { get; init; }

    public required IMacroNameModelList WareNames { get; init; }
}
