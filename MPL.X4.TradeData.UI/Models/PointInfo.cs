namespace MPL.X4.TradeData.UI.Models;

internal class PointInfo
{
    public string? Description { get; init; }

    public string? Name { get; init; }

    public bool ShowTooltip { get; init; } = false;

    public required double X { get; init; }

    public required double Y { get; init; }

    public required double Z { get; init; }
}
