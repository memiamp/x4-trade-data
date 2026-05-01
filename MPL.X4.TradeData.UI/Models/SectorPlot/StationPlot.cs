namespace MPL.X4.TradeData.UI.Models.SectorPlot;

/// <summary>
/// A class that implements a sector plot for a <see cref="IStation"/>.
/// </summary>
/// <param name="item">An <see cref="IStation"/> to be plotted.</param>
internal class StationPlot(
                           IStation item)
    : ISectorPlot
{
    string? ISectorPlot.ColourId => $"faction_{item.Owner}";

    string ISectorPlot.Name => $"{item.NameId}";

    SectorPlotType ISectorPlot.Type => SectorPlotType.Station;

    double ISectorPlot.X => item.Position.X;

    double ISectorPlot.Y => item.Position.Y;

    double ISectorPlot.Z => item.Position.Z;
}
