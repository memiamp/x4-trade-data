namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a station.
/// </summary>
public interface IStationData
{
    /// <summary>
    /// Gets the code of the station.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the identifier of the station.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets an indication of whether the station is known to the player.
    /// </summary>
    bool IsKnown { get; }

    /// <summary>
    /// Gets the name identifier of the station.
    /// </summary>
    ITextResourceReference? NameId { get; }

    /// <summary>
    /// Gets the owner of the station.
    /// </summary>
    string Owner { get; }

    /// <summary>
    /// Gets the position of the station.
    /// </summary>
    IPosition3D Position { get; }

    /// <summary>
    /// Gets the trades on offer at the station.
    /// </summary>
    IEnumerable<ITradeData> Trades { get; }
}
