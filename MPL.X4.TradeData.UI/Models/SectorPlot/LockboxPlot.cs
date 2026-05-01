namespace MPL.X4.TradeData.UI.Models.SectorPlot;

/// <summary>
/// A class that implements a sector plot for a <see cref="ILockbox"/>.
/// </summary>
/// <param name="item">An <see cref="ILockbox"/> to be plotted.</param>
internal class LockboxPlot(
                           ILockbox item)
    : ISectorPlot
{
    string? ISectorPlot.ColourId => "azure_glow";

    string ISectorPlot.Name => $"{item.Code} ({item.LockCount})";

    SectorPlotType ISectorPlot.Type => SectorPlotType.Lockbox;

    double ISectorPlot.X => item.Position.X;

    double ISectorPlot.Y => item.Position.Y;

    double ISectorPlot.Z => item.Position.Y;
}
