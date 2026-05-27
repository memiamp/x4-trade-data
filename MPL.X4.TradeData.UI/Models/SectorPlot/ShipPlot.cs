using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.SectorPlot;

/// <summary>
/// A class that implements a sector plot for an <see cref="IShipModel"/>.
/// </summary>
/// <param name="item">An <see cref="IShipModel"/> to be plotted.</param>
internal class ShipPlot(
                        IShipModel item)
    : ISectorPlot
{
    Color ISectorPlot.Colour
        => (item.CanBeCaptured, item.IsWreck) switch
        {
            (true, _) => Constants.Colours.AbandonedShip,
            (_, true) => Constants.Colours.Wreck,
            _ => item.Owner.Colour.Colour
        };

    string ISectorPlot.Name => item.Name ?? string.Empty;

    SectorPlotType ISectorPlot.Type => SectorPlotType.Ship;

    double ISectorPlot.X => item.Transform.Position.X;

    double ISectorPlot.Y => item.Transform.Position.Y;

    double ISectorPlot.Z => item.Transform.Position.Z;
}
