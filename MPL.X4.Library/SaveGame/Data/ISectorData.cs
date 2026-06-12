namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a sector.
/// </summary>
public interface ISectorData
{
    /// <summary>
    /// Gets the code of the sector.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the highways in the sector.
    /// </summary>
    IEnumerable<IHighwayData> Highways { get; }

    /// <summary>
    /// Gets the identifier of the sector.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets an indication of whether the sector is known to the player.
    /// </summary>
    bool IsKnown { get; }

    /// <summary>
    /// Gets the macro of the sector.
    /// </summary>
    string Macro { get; }

    /// <summary>
    /// Gets the owner of the sector.
    /// </summary>
    string Owner { get; }

    /// <summary>
    /// Gets the zones in the sector.
    /// </summary>
    IEnumerable<IZoneData> Zones { get; }
}
