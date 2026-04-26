namespace MPL.X4.TradeData.UI.Models;

/// <summary>
/// A class that implements a model for a ware.
/// </summary>
public class Ware
{
    /// <summary>
    /// Gets the average universe buy price of the ware.
    /// </summary>
    public long AverageBuyPrice { get; set; }

    /// <summary>
    /// Gets the average universe sell price of the ware.
    /// </summary>
    public long AverageSellPrice { get; set; }

    /// <summary>
    /// Gets the name of the ware.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets the total buying volume of the ware.
    /// </summary>
    public int TotalBuyAvailability { get; set; }

    /// <summary>
    /// Gets the total selling volume of the ware.
    /// </summary>
    public int TotalSellAvailability { get; set; }
}
