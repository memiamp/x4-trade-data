namespace MPL.X4.TradeData.UI.Models;

/// <summary>
/// A class that implements a model for a special item.
/// </summary>
public class SpecialItem
{
    /// <summary>
    /// Gets or sets the code of the special item.
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Gets or sets the description of the special item.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the name of the sector the special item is located in.
    /// </summary>
    public required string SectorName { get; set; }

    /// <summary>
    /// Gets or sets the type of the special item.
    /// </summary>
    public required SpecialItemType Type { get; set; }

    /// <summary>
    /// Gets or sets the X position of the special item in the sector.
    /// </summary>
    public required string X { get; set; }

    /// <summary>
    /// Gets or sets the Yvposition of the special item in the sector.
    /// </summary>
    public required string Y { get; set; }

    /// <summary>
    /// Gets or sets the Z position of the special item in the sector.
    /// </summary>
    public required string Z { get; set; }
}
