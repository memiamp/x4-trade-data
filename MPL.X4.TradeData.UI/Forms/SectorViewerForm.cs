using System.ComponentModel;
using MPL.X4.TradeData.UI.Models;
using ScottPlot;

namespace MPL.X4.TradeData.UI.Forms;

public partial class SectorViewerForm : Form
{
    #region Declarations

    private ISector? _sector;

    private PlotData? _abandonedShipPlots;
    private PlotData? _gatePlots;
    private PlotData? _lockboxPlots;
    private PlotData? _stationPlots;
    private PlotData? _shipPlots;

    #endregion

    #region Constructors

    public SectorViewerForm()
    {
        InitializeComponent();
        Initialise();
    }

    #endregion

    #region Methods

    private PointInfo GeneratePoint(IGate source)
    {
        return new PointInfo
        {
            X = source.Position.X,
            Y = source.Position.Y,
            Z = source.Position.Z
        };
    }

    private PointInfo GeneratePoint(ILockbox source)
    {
        return new PointInfo
        {
            Name = source.Type,
            ShowTooltip = true,
            X = source.Position.X,
            Y = source.Position.Y,
            Z = source.Position.Z
        };
    }

    private PointInfo GeneratePoint(IShip source, bool isAbandoned = false)
    {
        return new PointInfo
        {
            Name = isAbandoned ? source.Macro : null,
            ShowTooltip = isAbandoned,
            X = source.Position.X,
            Y = source.Position.Y,
            Z = source.Position.Z
        };
    }

    private PointInfo GeneratePoint(IStation source)
    {
        return new PointInfo
        {
            Name = source.NameId?.ToString(),
            ShowTooltip = true,
            X = source.Position.X,
            Y = source.Position.Y,
            Z = source.Position.Z
        };
    }

    private double GetMaxPlotSize()
    {
        var allPlots = new[]
        {
            _abandonedShipPlots,
            _lockboxPlots,
            _shipPlots,
            _stationPlots
        };

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return allPlots
                       .Where(p => p != null)
                       .SelectMany(p => new[] { p.GetMaxX(), p.GetMaxZ() })
                       .DefaultIfEmpty(0)
                       .Max();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private void Initialise()
    {
    }

    private static PlotData LoadPlots(Func<IEnumerable<PointInfo>?> generator)
        => new()
        {
            Points = generator() ?? []
        };

    private void LoadSectorData()
    {
        _abandonedShipPlots = LoadPlots(() => _sector?
                                                      .Ships
                                                      .Where(x => x.Owner == Constants.SaveGameFile.AttributeValue.Owner.Ownerless)
                                                      .Select(x => GeneratePoint(x, true)));
        _gatePlots = LoadPlots(() => _sector?
                                            .Gates
                                            .Select(GeneratePoint));

        _lockboxPlots = LoadPlots(() => _sector?
                                                .Lockboxes
                                                .Select(GeneratePoint));

        _shipPlots = LoadPlots(() => _sector?
                                             .Ships
                                             .Where(x => x.Owner != Constants.SaveGameFile.AttributeValue.Owner.Ownerless)
                                             .Select(x => GeneratePoint(x)));

        _stationPlots = LoadPlots(() => _sector?
                                                .Stations
                                                .Select(GeneratePoint));

        PlotSectorData();
    }

    private void PlotPoints(PlotData? plotData, string name, ScottPlot.Color colour, MarkerShape shape, int markerSize)
    {
        if (plotData?.Points.Any() == true)
        {
            var xPoints = plotData.Points.Select(x => x.X).ToArray();
            var zPoints = plotData.Points.Select(x => x.Z).ToArray();

            var plot = SectorPlot.Plot.Add.ScatterPoints(xPoints, zPoints);
            plot.Color = colour;
            plot.MarkerShape = shape;
            plot.MarkerSize = markerSize;
            plot.LegendText = name;
        }
    }

    private void PlotSectorData()
    {
        SetupPlotStyle();

        PlotPoints(_gatePlots, "Gates", Colors.Gray, MarkerShape.OpenCircleWithCross, 8);

        PlotPoints(_stationPlots, "Stations", Colors.Red, MarkerShape.FilledSquare, 10);

        PlotPoints(_shipPlots, "Ships", Colors.Blue, MarkerShape.FilledDiamond, 5);

        PlotPoints(_abandonedShipPlots, "Abandoned Ships", Colors.Green, MarkerShape.FilledCircle, 8);

        PlotPoints(_lockboxPlots, "Lockboxes", Colors.DarkGoldenRod, MarkerShape.FilledTriangleUp, 10);
    }

    private void SetupPlotStyle()
    {
        var maxDimension = GetMaxPlotSize();
        maxDimension *= 1.15;

        SectorPlot.Plot.Clear();

        SectorPlot.Plot.Axes.SetLimits(-maxDimension, maxDimension, -maxDimension, maxDimension);

        // Setup 0,0 lines
        var vLine = SectorPlot.Plot.Add.VerticalLine(0);
        var hLine = SectorPlot.Plot.Add.HorizontalLine(0);

        vLine.Color = ScottPlot.Colors.Black;
        vLine.EnableAutoscale = false;
        vLine.LinePattern = ScottPlot.LinePattern.Dashed;
        vLine.LineWidth = 2;

        hLine.EnableAutoscale = false;
        hLine.Color = ScottPlot.Colors.Black;
        hLine.LinePattern = ScottPlot.LinePattern.Dashed;
        hLine.LineWidth = 2;

        // Setup axes
        var tickGen = new ScottPlot.TickGenerators.NumericFixedInterval(50000)
        {
            LabelFormatter = pos =>
            {
                if (pos == 0) return "0";
                double abs = Math.Abs(pos);
                return (pos < 0 ? "-" : "") +
                       (abs >= 1000000 ? (abs / 1000000).ToString("0.#") + "M" : (abs / 1000).ToString("0.#") + "k");
            }
        };
        SectorPlot.Plot.Axes.Bottom.TickGenerator = tickGen;
        SectorPlot.Plot.Axes.Left.TickGenerator = tickGen;

        // Setup grid display
        SectorPlot.Plot.Grid.IsVisible = true;
        SectorPlot.Plot.Grid.LineColor = Colors.LightGray.WithAlpha(0.5);
        SectorPlot.Plot.Title($"Sector: {_sector?.Macro}");
        SectorPlot.Plot.XLabel("X Coordinate");
        SectorPlot.Plot.YLabel("Z Coordinate");
    }

    #endregion

    #region Properties

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

    #endregion
}
