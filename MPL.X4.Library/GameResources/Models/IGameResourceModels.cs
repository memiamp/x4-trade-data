namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines the behaviour of game resource data.
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
    /// Gets sector names.
    /// </summary>
    ISectorNameModelList SectorNames { get; }

    /// <summary>
    /// Gets text resource models.
    /// </summary>
    ITextResourceModelList Text { get; }

    /// <summary>
    /// Gets zone offset resource data.
    /// </summary>
    //IZoneOffsetDataDictionary ZoneOffsets { get; }
}
