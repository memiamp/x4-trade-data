using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.SectorPlot;

/// <summary>
/// A class that implements a sector plot for a <see cref="IGateModel"/>.
/// </summary>
/// <param name="item">An <see cref="IGateModel"/> to be plotted.</param>
internal class GatePlot(
                        IGateModel item)
    : ISectorPlot
{
    Color ISectorPlot.Colour => Constants.Colours.Gate;

    string ISectorPlot.Name => $"{item.Code}";

    SectorPlotType ISectorPlot.Type
        => item.Type switch
        {
            GateType.JumpGate => SectorPlotType.JumpGate,
            GateType.Superhighway => SectorPlotType.Superhighway,
            GateType.TransorbitalAccelerator => SectorPlotType.TransorbitalAccelerator,
            _ => SectorPlotType.JumpGate
        };

    double ISectorPlot.X => item.Transform.Position.X;

    double ISectorPlot.Y => item.Transform.Position.Y;

    double ISectorPlot.Z => item.Transform.Position.Z;
}
