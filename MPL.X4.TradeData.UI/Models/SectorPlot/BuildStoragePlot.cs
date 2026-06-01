using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.SectorPlot;

/// <summary>
/// A class that implements a sector plot for a <see cref="IBuildStorageModel"/>.
/// </summary>
/// <param name="item">An <see cref="IBuildStorageModel"/> to be plotted.</param>
internal class BuildStoragePlot(
                                IBuildStorageModel item)
    : ISectorPlot
{
    Color ISectorPlot.Colour
        => item.IsWreck switch
        {
            true => Constants.Colours.Wreck,
            _ => item.Owner.Colour.Colour
        };

    string ISectorPlot.Name => item.Code;

    SectorPlotType ISectorPlot.Type => SectorPlotType.BuildStorage;

    double ISectorPlot.X => item.Transform.Position.X;

    double ISectorPlot.Y => item.Transform.Position.Y;

    double ISectorPlot.Z => item.Transform.Position.Z;
}
