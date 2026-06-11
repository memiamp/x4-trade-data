namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An enumeration that defines the type of a trade partner.
/// </summary>
public enum TradePartnerType
{
    /// <summary>
    /// The type is unknown.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The trade partner is a build storage.
    /// </summary>
    BuildStorage,

    /// <summary>
    /// The trade partner is a removed entity.
    /// </summary>
    Removed,

    /// <summary>
    /// The trade partner is a ship.
    /// </summary>
    Ship,

    /// <summary>
    /// The trade partner is a station.
    /// </summary>
    Station
}