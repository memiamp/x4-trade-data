using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using MPL.X4.TradeData.UI.Models;
using MPL.X4.TradeData.UI.Models.SectorPlot;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a control that plots sector data.
/// </summary>
internal class PlotControl : Panel
{
    #region Declarations

    private readonly List<ISectorPlot> _items = [];
    private readonly Dictionary<SectorPlotType, Bitmap> _iconCache = [];
    private readonly PointF[] _logicalHexPoints =
    [
        new(-200,  350),
        new( 200,  350),
        new( 400,    0),
        new( 200, -350),
        new(-200, -350),
        new(-400,    0)
    ];
   // private readonly double _maxPixelxPerMeter = 20;
    private readonly ToolTip _tooltip = new() { InitialDelay = 200, AutoPopDelay = 8000 };

    private bool _isDragging = false;
    private ISectorPlot? _lastHovered = null;
    private DateTime _lastInvalidate = DateTime.MinValue;
    private Point _lastMousePos;
    private double _maxCenterX;
    private double _maxCenterY;
    private double _maxPixelsPerMeter = 10.0;
    private double _minCenterX;
    private double _minCenterY;
    private double _minPixelsPerMeter = 0.001;
    private double _pixelsPerMeter = 0.02;
    private double _viewCenterX = 0;
    private double _viewCenterY = 0;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="PlotControl"/> class.
    /// </summary>
    public PlotControl()
    {
        Initialise();
    }

    #endregion

    #region Methods
    #region _Internal_

    /// <summary>
    /// Adds the specified <paramref name="item"/> to the plot.
    /// </summary>
    /// <param name="item">An <see cref="ISectorPlot"/> to be added.</param>
    internal void AddItem(ISectorPlot item)
    {
        _items.Add(item);
        UpdateZoomLimits();
        Invalidate();
    }

    /// <summary>
    /// Adds the specified <paramref name="items"/> to the plot.
    /// </summary>
    /// <param name="items">A nullable <see cref="IEnumerable{T}"/> of <see cref="ISectorPlot"/> containing the items to be added.</param>
    internal void AddRange(IEnumerable<ISectorPlot>? items)
    {
        if (items?.Any() == true)
        {
            _items.AddRange(items);
            UpdateZoomLimits();
            Invalidate();
        }
    }

    /// <summary>
    /// Clears all items from the plot.
    /// </summary>
    internal void Clear()
    {
        _items.Clear();
        UpdateZoomLimits();
        Invalidate();
    }

    /// <summary>
    /// Immediately refreshes the plot.
    /// </summary>
    internal void RefreshPlot()
        => Invalidate();

    /// <summary>
    /// Resets the current view to the default (zoomed out).
    /// </summary>
    internal void ResetView()
    {
        UpdateZoomLimits();
        UpdateZoomLimits();

        if (_items.Count > 0)
        {
            _viewCenterX = (_items.Min(i => i.X) + _items.Max(i => i.X)) / 2.0;
            _viewCenterY = (_items.Min(i => i.Z) + _items.Max(i => i.Z)) / 2.0;
        }

        _pixelsPerMeter = _minPixelsPerMeter;

        ClampViewCenter();

        Invalidate();
    }

    #endregion
    #region _Private_

    private void ClampViewCenter()
    {
        _viewCenterX = Math.Clamp(_viewCenterX, _minCenterX, _maxCenterX);
        _viewCenterY = Math.Clamp(_viewCenterY, _minCenterY, _maxCenterY);
    }

    private void DrawContent(Graphics g, PointF leftPoint, PointF topPoint)
    {
        double halfW = Width / 2.0 / _pixelsPerMeter;
        double halfH = Height / 2.0 / _pixelsPerMeter;

        double minX = _viewCenterX - halfW;
        double maxX = _viewCenterX + halfW;
        double minY = _viewCenterY - halfH;
        double maxY = _viewCenterY + halfH;

        double gridStep = GetGridStep(_pixelsPerMeter);
        var zero = WorldToScreen(0, 0);

        using var labelFont = new Font("Segoe UI", 9f, FontStyle.Regular);
        using var labelBrush = new SolidBrush(Color.FromArgb(128, 180, 255, 100)); // light cyan, semi-transparent

        using var gridPen = new Pen(Color.FromArgb(128, Color.LightGray), 1f);

        for (double x = Math.Ceiling(minX / gridStep) * gridStep; x <= maxX; x += gridStep)
        {
            if (x != 0)
            {
                var p1 = WorldToScreen(x, minY);
                var p2 = WorldToScreen(x, maxY);
                g.DrawLine(gridPen, p1, p2);

                string xText = FormatDistance(x, gridStep);
                var labelPt = WorldToScreen(x, minY);
                g.DrawString(xText, labelFont, labelBrush, labelPt.X, topPoint.Y);
            }
        }

        for (double y = Math.Ceiling(minY / gridStep) * gridStep; y <= maxY; y += gridStep)
        {
            if (y != 0)
            {
                var p1 = WorldToScreen(minX, y);
                var p2 = WorldToScreen(maxX, y);
                g.DrawLine(gridPen, p1, p2);

                string yText = FormatDistance(0 - y, gridStep);
                var labelPt = WorldToScreen(_viewCenterX - 200, y);
                g.DrawString(yText, labelFont, labelBrush, leftPoint.X, labelPt.Y);
            }
        }

        float thickness = Math.Max(1.5f, 3f / (float)_pixelsPerMeter); // gets thicker when zoomed in
        using var axisPen = new Pen(Color.FromArgb(128, Color.LimeGreen), 2f) { DashStyle = DashStyle.Dash };

        if (zero.X > -50 && zero.X < Width + 50)
        {
            g.DrawLine(axisPen, new PointF(zero.X, 0), new PointF(zero.X, Height));  // Y axis

            using var font = new Font("Segoe UI", 9f);
            using var brush = new SolidBrush(Color.FromArgb(128, Color.LimeGreen));
        }

        if (zero.Y > -50 && zero.Y < Height + 50)
        {
            g.DrawLine(axisPen, new PointF(0, zero.Y), new PointF(Width, zero.Y));   // X axis
        }

        if (zero.X > -50 && zero.X < Width + 50 && zero.Y > -50 && zero.Y < Height + 50)
        {
            using var font = new Font("Segoe UI", 9f);
            using var brush = new SolidBrush(Color.FromArgb(128, Color.LimeGreen));

            g.DrawString("(0,0)", font, brush, zero.X + 6, zero.Y + 6);
        }

        const int iconSize = 16;
        const int half = iconSize / 2;

        foreach (var item in _items)
        {
            if (item.X < minX || item.X > maxX || item.Z < minY || item.Z > maxY)
                continue;

            var screen = WorldToScreen(item.X, item.Z);

            if (_iconCache.TryGetValue(item.Type, out var bmp))
            {
                var destRect = new Rectangle(
                        (int)(screen.X - half),
                        (int)(screen.Y - half),
                        iconSize,
                        iconSize);

                using var attributes = new ImageAttributes();

                var tintColor = Color.Pink;
                if (!string.IsNullOrWhiteSpace(item.ColourId) &&
                    ColourMap.TryGetValue(item.ColourId, out var colour))
                {
                    tintColor = Color.FromArgb(colour.Alpha, colour.Red, colour.Green, colour.Blue);
                }

                float r = tintColor.R / 255f;
                float gr = tintColor.G / 255f;
                float b = tintColor.B / 255f;

                var colorMatrix = new ColorMatrix(
                [
                    [r, 0, 0, 0, 0],
                    [0, gr, 0, 0, 0],
                    [0, 0, b, 0, 0],
                    [0, 0, 0, 1, 0],
                    [0, 0, 0, 0, 1]
                ]);

                attributes.SetColorMatrix(colorMatrix);

                g.DrawImage(
                            bmp,
                            destRect,
                            0,
                            0,
                            bmp.Width,
                            bmp.Height,
                            GraphicsUnit.Pixel,
                            attributes);
            }
            else
            {
                using var brush = new SolidBrush(Color.DodgerBlue);
                g.FillEllipse(brush, screen.X - 6, screen.Y - 6, 12, 12);
            }
        }

    }

    private static string FormatDistance(double metres, double gridStep)
    {
        double km = Math.Abs(metres) / 1000.0;
        string sign = metres < 0 ? "-" : "";

        if (Math.Abs(gridStep) < 1000)
            return $"{sign}{km:0.0}km";

        return $"{sign}{km:F0}km";
    }

    private static double GetGridStep(double pixelsPerMeter)
    {
        const double target = 80.0;
        double ideal = target / pixelsPerMeter;

        if (ideal <= 0) return 1.0;

        double log = Math.Log10(ideal);
        double pow = Math.Pow(10, Math.Floor(log));
        double frac = ideal / pow;

        double step;
        if (frac <= 1) step = pow;
        else if (frac <= 2) step = 2 * pow;
        else if (frac <= 5) step = 5 * pow;
        else step = 10 * pow;

        return step;
    }

    private ISectorPlot? HitTest(Point screenPoint)
    {
        const float hitRadius = 18f;   // generous for icons

        foreach (var item in _items)
        {
            var screen = WorldToScreen(item.X, item.Z);
            float dx = screen.X - screenPoint.X;
            float dy = screen.Y - screenPoint.Y;

            if (dx * dx + dy * dy < hitRadius * hitRadius)
                return item;
        }
        return null;
    }

    private void Initialise()
    {
        DoubleBuffered = true;
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.ResizeRedraw, true);

        BackColor = Color.Black;
        Cursor = Cursors.Cross;

        MouseDown += OnMouseDown;
        MouseLeave += OnMouseLeave;
        MouseMove += OnMouseMove;
        MouseUp += OnMouseUp;
        MouseWheel += OnMouseWheel;

        _iconCache[SectorPlotType.JumpGate] = MapIcons.Icon_JumpGate;
        _iconCache[SectorPlotType.Lockbox] = MapIcons.Icon_Lockbox;
        _iconCache[SectorPlotType.Ship] = MapIcons.Icon_Ship;
        _iconCache[SectorPlotType.Station] = MapIcons.Icon_Station;
        _iconCache[SectorPlotType.Superhighway] = MapIcons.Icon_Superhighway;
        _iconCache[SectorPlotType.TransorbitalAccelerator] = MapIcons.Icon_Transorbital;
    }

    private void UpdateZoomLimits()
    {
        _maxPixelsPerMeter = Math.Max(0.1, Math.Min(Width, Height) / 1000.0);

        if (_items.Count == 0)
        {
            _minPixelsPerMeter = 0.0005;
            _minCenterX = _minCenterY = -1_000_000;
            _maxCenterX = _maxCenterY = 1_000_000;
            return;
        }

        double dataMinX = _items.Min(i => i.X);
        double dataMaxX = _items.Max(i => i.X);
        double dataMinY = _items.Min(i => i.Z);
        double dataMaxY = _items.Max(i => i.Z);

        double rangeX = dataMaxX - dataMinX;
        double rangeY = dataMaxY - dataMinY;

        double maxRange = Math.Max(rangeX, rangeY);

        double paddedSquareSide = maxRange * 1.4;

        double fitPPM = Math.Min(Width, Height) / paddedSquareSide;
        _minPixelsPerMeter = fitPPM * 0.95;

        double visibleHalfSize = (Math.Min(Width, Height) / _minPixelsPerMeter) / 2.0;

        double centerX = (dataMinX + dataMaxX) / 2.0;
        double centerY = (dataMinY + dataMaxY) / 2.0;

        _minCenterX = centerX - visibleHalfSize * 1.1;
        _maxCenterX = centerX + visibleHalfSize * 1.1;
        _minCenterY = centerY - visibleHalfSize * 1.1;
        _maxCenterY = centerY + visibleHalfSize * 1.1;

        if (_minCenterX > _maxCenterX) (_minCenterX, _maxCenterX) = (_maxCenterX, _minCenterX);
        if (_minCenterY > _maxCenterY) (_minCenterY, _maxCenterY) = (_maxCenterY, _minCenterY);
    }

    private PointF WorldToScreen(double worldX, double worldY)
    {
        double screenX = (worldX - _viewCenterX) * _pixelsPerMeter + Width / 2.0;
        double screenY = (worldY - _viewCenterY) * _pixelsPerMeter + Height / 2.0;

        return new PointF((float)screenX, (float)screenY);
    }

    #endregion
    #region _Protected_

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (var item in _iconCache.Values)
                item?.Dispose();

            _iconCache.Clear();
            _tooltip.Dispose();
        }

        base.Dispose(disposing);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.Clear(Parent?.BackColor ?? Color.Gray);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        float scale = Math.Min(g.ClipBounds.Width / 800f, g.ClipBounds.Height / 700f);
        float ox = g.ClipBounds.X + g.ClipBounds.Width / 2f;
        float oy = g.ClipBounds.Y + g.ClipBounds.Height / 2f;

        var hexPoints = new PointF[_logicalHexPoints.Length];
        for (int i = 0; i < _logicalHexPoints.Length; i++)
        {
            hexPoints[i] = new PointF(
                                      _logicalHexPoints[i].X * scale + ox,
                                      _logicalHexPoints[i].Y * scale + oy);
        }

        using var path = new GraphicsPath();
        path.AddPolygon(hexPoints);

        using var region = new Region(path);

        g.Clip = region;

        using var bgBrush = new SolidBrush(Color.FromArgb(20, 20, 20)); // dark background
        g.FillPath(bgBrush, path);

        DrawContent(g, hexPoints[4], hexPoints[3]);

        g.ResetClip();

        using var borderPen = new Pen(Color.DodgerBlue, 4f);
        g.DrawPolygon(borderPen, hexPoints);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        UpdateZoomLimits();

        Invalidate();
    }

    #endregion
    #endregion

    #region Event Handlers

    private void OnMouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _isDragging = true;
            _lastMousePos = e.Location;
            Cursor = Cursors.SizeAll;
        }
    }

    private void OnMouseLeave(object? sender, EventArgs e)
    {
        _tooltip.Hide(this);
        _lastHovered = null;
    }

    private void OnMouseMove(object? sender, MouseEventArgs e)
    {
        if (_isDragging)
        {
            double dx = (e.X - _lastMousePos.X) / _pixelsPerMeter;
            double dy = (e.Y - _lastMousePos.Y) / _pixelsPerMeter;

            _viewCenterX -= dx;
            _viewCenterY -= dy;

            ClampViewCenter();

            _lastMousePos = e.Location;

            if ((DateTime.Now - _lastInvalidate).TotalMilliseconds > 16)
            {
                Invalidate();
                _lastInvalidate = DateTime.Now;
            }
            return;
        }

        var hovered = HitTest(e.Location);
        if (hovered != _lastHovered)
        {
            if (hovered != null)
            {
                string text = hovered.Name ??
                                $"X: {hovered.X:F1} m\nY: {hovered.Z:F1} m\nType: {hovered.Type}";

                _tooltip.Show(text, this, e.X + 18, e.Y + 18);
            }
            else
            {
                _tooltip.Hide(this);
            }

            _lastHovered = hovered;
        }
    }

    private void OnMouseUp(object? sender, MouseEventArgs e)
    {
        _isDragging = false;
        Cursor = Cursors.Cross;
    }

    private void OnMouseWheel(object? sender, MouseEventArgs e)
    {
        double factor = e.Delta > 0 ? 1.25 : 0.80;

        double newPixelsPerMeter = _pixelsPerMeter * factor;

        newPixelsPerMeter = Math.Clamp(newPixelsPerMeter, _minPixelsPerMeter, _maxPixelsPerMeter);

        _pixelsPerMeter = newPixelsPerMeter;

        ClampViewCenter();

        Invalidate();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the colour map to use.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Dictionary<string, IColour> ColourMap { get; set; } = [];

    #endregion
}
