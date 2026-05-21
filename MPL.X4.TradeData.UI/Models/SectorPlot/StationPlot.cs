using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.SectorPlot;

/// <summary>
/// A class that implements a sector plot for a <see cref="IStationModel"/>.
/// </summary>
/// <param name="item">An <see cref="IStationModel"/> to be plotted.</param>
internal class StationPlot(
                           IStationModel item)
    : ISectorPlot
{
    Color ISectorPlot.Colour
        => item.IsWreck switch
        {
            true => Constants.Colours.Wreck,
            _ => item.Owner.Colour.Colour
        };

    string ISectorPlot.Name => item.Name;

    SectorPlotType ISectorPlot.Type => SectorPlotType.Station;

    double ISectorPlot.X => item.Transform.Position.X;

    double ISectorPlot.Y => item.Transform.Position.Y;

    double ISectorPlot.Z => item.Transform.Position.Z;
}
