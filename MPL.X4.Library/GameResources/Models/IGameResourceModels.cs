namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines the behaviour of game resource models.
/// </summary>
public interface IGameResourceModels
{
    /// <summary>
    /// Gets colour resource models.
    /// </summary>
    IColourModelList Colours { get; }

    /// <summary>
    /// Gets faction resource models.
    /// </summary>
    IFactionModelList Factions { get; }

    /// <summary>
    /// Gets landmark names.
    /// </summary>
    IMacroNameModelList LandmarkNames { get; }

    /// <summary>
    /// Gets the offset resource models.
    /// </summary>
    IOffsetModelReference Offsets { get; }

    /// <summary>
    /// Gets sector names.
    /// </summary>
    IMacroNameModelList SectorNames { get; }

    /// <summary>
    /// Gets ship models.
    /// </summary>
    IMacroNameModelList ShipModels { get; }

    /// <summary>
    /// Gets text resource models.
    /// </summary>
    ITextResourceModelList Text { get; }

    /// <summary>
    /// Gets ware names.
    /// </summary>
    IMacroNameModelList WareNames { get; }
}
