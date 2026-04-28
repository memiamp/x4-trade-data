namespace MPL.X4.TradeData.UI.Models;

internal class PlotData
{
    internal double GetMaxX()
        => Points.Any()
                        ? Points.Max(x => Math.Abs(x.X))
                        : 0;

    internal double GetMaxZ()
        => Points.Any()
                        ? Points.Max(x => Math.Abs(x.Z))
                        : 0;

    internal IEnumerable<PointInfo> Points { get; set; } = [];
}
