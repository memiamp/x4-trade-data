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
    /// Gets offset resource data.
    /// </summary>
    IOffsetDataDictionary Offsets { get; }

    /// <summary>
    /// Gets sector name data.
    /// </summary>
    ISectorNameDataDictionary SectorNames { get; }

    /// <summary>
    /// Gets text resource data.
    /// </summary>
    ITextResourcePageDictionary Text { get; }
}
