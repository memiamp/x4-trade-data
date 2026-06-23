namespace MPL.X4.GameResources.Data;

/// <summary>
/// An interface that defines the behaviour of game resource data.
/// </summary>
public interface IGameResourceData
{
    /// <summary>
    /// Gets colour resource data.
    /// </summary>
    IColourResourceData Colours { get; }

    /// <summary>
    /// Gets faction resource data.
    /// </summary>
    IFactionDataDictionary Factions { get; }

    /// <summary>
    /// Gets landmark name data.
    /// </summary>
    IMacroNameResourceDataDictionary LandmarkNames { get; }

    /// <summary>
    /// Gets offset resource data.
    /// </summary>
    IOffsetDataDictionary Offsets { get; }

    /// <summary>
    /// Gets sector name data.
    /// </summary>
    IMacroNameResourceDataDictionary SectorNames { get; }

    /// <summary>
    /// Gets ship model data.
    /// </summary>
    IShipModelResourceDataDictionary ShipModels { get; }

    /// <summary>
    /// Gets text resource data.
    /// </summary>
    ITextResourcePageDictionary Text { get; }

    /// <summary>
    /// Gets wares data.
    /// </summary>
    IWareDataDictionary Wares { get; }
}
