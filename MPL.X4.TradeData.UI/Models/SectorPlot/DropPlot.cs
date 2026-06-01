using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.SectorPlot;

/// <summary>
/// A class that implements a sector plot for a <see cref="ICollectableDropModel"/>.
/// </summary>
/// <param name="item">An <see cref="ICollectableDropModel"/> to be plotted.</param>
internal class DropPlot(
                        ICollectableDropModel item)
    : ISectorPlot
{
    Color ISectorPlot.Colour => Constants.Colours.Collectable;

    string ISectorPlot.Name => $"{item.Name} ({item.Type})";

    SectorPlotType ISectorPlot.Type
        => item.Type == DropType.Ammo
                                      ? SectorPlotType.AmmoDrop
                                      : SectorPlotType.WareDrop;


    double ISectorPlot.X => item.Transform.Position.X;

    double ISectorPlot.Y => item.Transform.Position.Y;

    double ISectorPlot.Z => item.Transform.Position.Z;
}
