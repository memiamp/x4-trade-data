namespace MPL.X4.TradeData.UI.Models.SpecialItem;

/// <summary>
/// An interface that defines a special item.
/// </summary>
public interface ISpecialItem
{
    /// <summary>
    /// Gets the display colour of the item.
    /// </summary>
    Color Colour { get; }

    /// <summary>
    /// Gets or sets the comments of the special item.
    /// </summary>
    string Comments { get; }

    /// <summary>
    /// Gets or sets the description of the special item.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Gets or sets the name of the sector the special item is located in.
    /// </summary>
    string SectorName { get; }

    /// <summary>
    /// Gets or sets the type of the special item.
    /// </summary>
    SpecialItemType Type { get; }

    /// <summary>
    /// Gets or sets the X position of the special item in the sector.
    /// </summary>
    string X { get; }

    /// <summary>
    /// Gets or sets the Yvposition of the special item in the sector.
    /// </summary>
    string Y { get; }

    /// <summary>
    /// Gets or sets the Z position of the special item in the sector.
    /// </summary>
    string Z { get; }
}
