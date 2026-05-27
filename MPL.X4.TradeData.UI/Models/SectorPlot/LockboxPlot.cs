using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models.SectorPlot;

/// <summary>
/// A class that implements a sector plot for a <see cref="ILockboxModel"/>.
/// </summary>
/// <param name="item">An <see cref="ILockboxModel"/> to be plotted.</param>
internal class LockboxPlot(
                           ILockboxModel item)
    : ISectorPlot
{
    Color ISectorPlot.Colour => Constants.Colours.Collectable;

    string ISectorPlot.Name => $"{item.Code} ({item.LockCount})";

    SectorPlotType ISectorPlot.Type => SectorPlotType.Lockbox;

    double ISectorPlot.X => item.Transform.Position.X;

    double ISectorPlot.Y => item.Transform.Position.Y;

    double ISectorPlot.Z => item.Transform.Position.Z;
}
