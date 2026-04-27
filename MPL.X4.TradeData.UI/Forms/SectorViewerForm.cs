using ScottPlot;
using System.ComponentModel;

namespace MPL.X4.TradeData.UI.Forms;

public partial class SectorViewerForm : Form
{
    private ISector? _sector;

    public SectorViewerForm()
    {
        InitializeComponent();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal ISector? Sector
    {
        get
        { 
            return _sector; }

        set
        {
            _sector = value;
            LoadSectorData();
        }
    }

    private void LoadSectorData()
    {
        PlotSectorData();
    }

    private void PlotSectorData()
    {
        formsPlot1.Plot.Clear(); // reset

        // Example: separate lists from your save parser (X/Z plane is typical for X4 2D maps)
        var shipXs = _sector?.Ships.Select(s => s.Position.X).ToArray() ?? [];
        var shipZs = _sector?.Ships.Select(s => s.Position.Z).ToArray() ?? [];

        var stationXs = _sector?.Stations.Select(s => s.Position.X).ToArray() ?? [];
        var stationZs = _sector?.Stations.Select(s => s.Position.Z).ToArray() ?? [];

        var lockboxXs = _sector?.Lockboxes.Select(l => l.Position.X).ToArray() ?? [];
        var lockboxZs = _sector?.Lockboxes.Select(l => l.Position.Z).ToArray() ?? [];

        // Different styles per type
        var shipsPlot = formsPlot1.Plot.Add.ScatterPoints(shipXs, shipZs);
        shipsPlot.Color = Colors.Blue;
        shipsPlot.MarkerShape = MarkerShape.TriUp;
        shipsPlot.MarkerSize = 8;
        shipsPlot.LegendText = "Ships";

        var stationsPlot = formsPlot1.Plot.Add.ScatterPoints(stationXs, stationZs);
        stationsPlot.Color = Colors.Red;
        stationsPlot.MarkerShape = MarkerShape.FilledSquare;
        stationsPlot.MarkerSize = 10;
        stationsPlot.LegendText = "Stations";

        var lockboxesPlot = formsPlot1.Plot.Add.ScatterPoints(lockboxXs, lockboxZs);
        lockboxesPlot.Color = Colors.Orange;
        lockboxesPlot.MarkerShape = MarkerShape.Cross;
        lockboxesPlot.MarkerSize = 7;
        lockboxesPlot.LegendText = "Lockboxes";

        // Optional: sector map background (if you have an image)
        // Image bg = new Image("MySectorMap.png");  // or load from resources
        // formsPlot1.Plot.DataBackground.Image = bg;

        // Nice grid + labels
        formsPlot1.Plot.Grid.IsVisible = true;
        formsPlot1.Plot.Grid.LineColor = Colors.LightGray.WithAlpha(0.5);
        formsPlot1.Plot.Title($"Sector: {_sector?.NameId}");
        formsPlot1.Plot.XLabel("X Coordinate");
        formsPlot1.Plot.YLabel("Z Coordinate");

        // Optional: lock view to sector bounds (prevents weird zooming out)
        //formsPlot1.Plot.Axes.SetLimits(minX, maxX, minZ, maxZ);

        formsPlot1.Refresh();
    }
}
