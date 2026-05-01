namespace MPL.X4.TradeData.UI.Models.SectorPlot;

/// <summary>
/// An interface that defines an item that can be plotted in a sector.
/// </summary>
internal interface ISectorPlot
{
    /// <summary>
    /// Gets the identifier of the colour this plot item should use.
    /// </summary>
    string? ColourId { get; }

    /// <summary>
    /// Gets the name of the plot item.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the type of the plot item.
    /// </summary>
    SectorPlotType Type { get; }

    /// <summary>
    /// Gets the X position of the plot item.
    /// </summary>
    double X { get; }

    /// <summary>
    /// Gets the y position of the plot item.
    /// </summary>
    double Y { get; }

    /// <summary>
    /// Gets the z position of the plot item.
    double Z { get; }
}
