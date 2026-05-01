namespace MPL.X4.TradeData.UI.Models.SectorPlot;

/// <summary>
/// A class that implements a sector plot for a <see cref="IShip"/>.
/// </summary>
/// <param name="item">An <see cref="IShip"/> to be plotted.</param>
internal class ShipPlot(
                        IShip item)
    : ISectorPlot
{
    string? ISectorPlot.ColourId => $"faction_{item.Owner}";
    
    string ISectorPlot.Name => $"{item.Class} {item.Macro}";

    SectorPlotType ISectorPlot.Type => SectorPlotType.Ship;

    double ISectorPlot.X => item.Position.X;

    double ISectorPlot.Y => item.Position.Y;

    double ISectorPlot.Z => item.Position.Y;
}
