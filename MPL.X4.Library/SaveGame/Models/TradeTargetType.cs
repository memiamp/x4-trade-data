namespace MPL.X4.SaveGame.Models;

/// <summary>
/// Gets the target type of a trade.
/// </summary>
public enum TradeTargetType
{
    /// <summary>
    /// The target type is unknown.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The target is a build storage.
    /// </summary>
    BuildStorage,

    /// <summary>
    /// The target is a station.
    /// </summary>
    Station
}
