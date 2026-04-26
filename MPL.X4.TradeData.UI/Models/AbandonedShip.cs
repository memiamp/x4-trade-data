namespace MPL.X4.TradeData.UI.Models;

/// <summary>
/// A class that implements a model for an abandoned ship.
/// </summary>
public class AbandonedShip
{
    /// <summary>
    /// Gets or sets the class of the ship.
    /// </summary>
    public ShipClass Class { get; set; }

    /// <summary>
    /// Gets or sets the name of the sector the ship is located in.
    /// </summary>
    public required string SectorName { get; set; }

    /// <summary>
    /// Gets or sets the type of the ship.
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Gets or sets the X position of the ship in the sector.
    /// </summary>
    public required string X { get; set; }

    /// <summary>
    /// Gets or sets the Yvposition of the ship in the sector.
    /// </summary>
    public required string Y { get; set; }

    /// <summary>
    /// Gets or sets the Z position of the ship in the sector.
    /// </summary>
    public required string Z { get; set; }
}
