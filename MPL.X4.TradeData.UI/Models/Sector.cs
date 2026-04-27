namespace MPL.X4.TradeData.UI.Models;

/// <summary>
/// A class that implements a model for a sector.
/// </summary>
public class Sector
{

    /// <summary>
    /// Gets or sets the number of abandoned ships items in the sector.
    /// </summary>
    public required int AbandonedShipCount { get; set; }

    /// <summary>
    /// Gets or sets the code of the sector.
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Gets or sets the number of lockbox items in the sector.
    /// </summary>
    public required int LockboxCount { get; set; }

    /// <summary>
    /// Gets or sets the name of the sector.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the number of ships items in the sector.
    /// </summary>
    public required int ShipCount { get; set; }

    /// <summary>
    /// Gets or sets the number of stations items in the sector.
    /// </summary>
    public required int StationCount { get; set; }
}
