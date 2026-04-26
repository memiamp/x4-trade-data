namespace MPL.X4.TradeData.UI.Models;

/// <summary>
/// A class that implements a model for an abandoned ship.
/// </summary>
public class TradeOffer
{
    /// <summary>
    /// Gets or sets the amount available to trade.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Get or sets the price of the trade.
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// Gets or sets the name of the sector the trade is located in.
    /// </summary>
    public required string SectorName { get; set; }

    /// <summary>
    /// Gets or sets the name of the station that trade is located in.
    /// </summary>
    public required string StationName { get; set; }

    /// <summary>
    /// Gets or sets the type of the trade.
    /// </summary>
    public TradeType Type { get; set; }

    /// <summary>
    /// Gets or sets the trade ware on offer.
    /// </summary>
    public required string Ware { get; set; }
}
