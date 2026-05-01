using System.Drawing.Drawing2D;

namespace MPL.X4.TradeData.UI.Controls;

public class PlotControl : Panel
{
    // ==================== Data & View State ====================
    public record PlotItem(double X, double Y, ItemType Type, string? Tooltip = null);

    public enum ItemType { Circle, Square, Ship, Plane /* add more as needed */ }

    private readonly List<PlotItem> _items = new();
   // private readonly Dictionary<ItemType, Bitmap> _iconCache = new();

    private double _viewCenterX = 250_000;   // metres
    private double _viewCenterY = 250_000;
    private double _pixelsPerMeter = 0.02;   // initial zoom (~50 m per pixel)

    private PlotItem? _lastHovered = null;
    private readonly ToolTip _tooltip = new() { InitialDelay = 200, AutoPopDelay = 8000 };

    // ==================== Constructor ====================
    public PlotControl()
    {
        DoubleBuffered = true;
        BackColor = Color.Black;
        Cursor = Cursors.Cross;

        // Mouse handlers
        MouseWheel += OnMouseWheel;
        MouseDown += OnMouseDown;
        MouseMove += OnMouseMove;
        MouseUp += OnMouseUp;
        MouseLeave += OnMouseLeave;

        LoadIcons();
    }

    // ==================== Public API ====================
    public void AddItem(PlotItem item)
    {
        _items.Add(item);
        Invalidate();
    }

    public void Clear() => _items.Clear();

    // Call this if you change items and want to refresh
    public void RefreshPlot() => Invalidate();

    // ==================== Icon Loading ====================
    private void LoadIcons()
    {
        // Option 1: From files (good for dev)
        string basePath = AppContext.BaseDirectory;
        //_iconCache[ItemType.Ship] = new Bitmap(Path.Combine(basePath, "Icons", "ship.png"));
       // _iconCache[ItemType.Plane] = new Bitmap(Path.Combine(basePath, "Icons", "plane.png"));
        // _iconCache[ItemType.Circle] = new Bitmap(...) etc.

        // Option 2: Embedded resources (better for release)
        // _iconCache[ItemType.Ship] = LoadResourceBitmap("MyApp.Icons.ship.png");
    }

    // private Bitmap LoadResourceBitmap(string resourceName) { ... } // see previous message

    // ==================== Coordinate Transforms ====================
    private PointF WorldToScreen(double worldX, double worldY)
    {
        double screenX = (worldX - _viewCenterX) * _pixelsPerMeter + Width / 2.0;
        double screenY = (worldY - _viewCenterY) * _pixelsPerMeter + Height / 2.0;
        return new PointF((float)screenX, (float)screenY);
    }

    private PointD ScreenToWorld(Point screenPoint)
    {
        double worldX = _viewCenterX + (screenPoint.X - Width / 2.0) / _pixelsPerMeter;
        double worldY = _viewCenterY + (screenPoint.Y - Height / 2.0) / _pixelsPerMeter;
        return new PointD(worldX, worldY);
    }

    public readonly record struct PointD(double X, double Y);

    // ==================== Grid Step ====================
    private double GetGridStep(double pixelsPerMeter)
    {
        const double targetSpacingPx = 80.0;
        double idealStep = targetSpacingPx / pixelsPerMeter;

        double magnitude = Math.Pow(10, Math.Floor(Math.Log10(idealStep)));
        double[] multipliers = { 1, 2, 5 };
        double multiplier = multipliers.FirstOrDefault(m => m * magnitude >= idealStep);

        return multiplier * magnitude;
    }

    // ==================== Hit Testing ====================
    private PlotItem? HitTest(Point screenPoint)
    {
        const float hitRadius = 18f;   // generous for icons

        foreach (var item in _items)
        {
            var screen = WorldToScreen(item.X, item.Y);
            float dx = screen.X - screenPoint.X;
            float dy = screen.Y - screenPoint.Y;

            if (dx * dx + dy * dy < hitRadius * hitRadius)
                return item;
        }
        return null;
    }
    private readonly PointF[] _logicalHexPoints = new PointF[]
{
    new PointF(-200,  350),   // A - top-left flat
    new PointF( 200,  350),   // B - top-right flat
    new PointF( 400,    0),   // C - rightmost point
    new PointF( 200, -350),   // D - bottom-right flat
    new PointF(-200, -350),   // E - bottom-left flat
    new PointF(-400,    0)    // F - leftmost point
};
    private void DrawHexagon(Graphics g, RectangleF bounds)
    {
        float scale = Math.Min(bounds.Width / 800f, bounds.Height / 700f);
        float ox = bounds.X + bounds.Width / 2f;
        float oy = bounds.Y + bounds.Height / 2f;

        PointF[] pts = new PointF[_logicalHexPoints.Length];
        for (int i = 0; i < _logicalHexPoints.Length; i++)
        {
            pts[i] = new PointF(
                _logicalHexPoints[i].X * scale + ox,
                _logicalHexPoints[i].Y * scale + oy);
        }

        using var pen = new Pen(Color.DarkBlue, Math.Max(3f, scale * 6f));
        using var brush = new SolidBrush(Color.Black);

        g.FillPolygon(brush, pts);
        g.DrawPolygon(pen, pts);
    }

    // ==================== Painting ====================
    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.Clear(Color.Transparent);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        DrawHexagon(g, g.ClipBounds);

        double halfW = Width / 2.0 / _pixelsPerMeter;
        double halfH = Height / 2.0 / _pixelsPerMeter;

        double minX = _viewCenterX - halfW;
        double maxX = _viewCenterX + halfW;
        double minY = _viewCenterY - halfH;
        double maxY = _viewCenterY + halfH;

        double gridStep = GetGridStep(_pixelsPerMeter);

        // === Grid ===
        using var gridPen = new Pen(Color.LightGray, 1f);

        // Vertical lines
        for (double x = Math.Ceiling(minX / gridStep) * gridStep; x <= maxX; x += gridStep)
        {
            var p1 = WorldToScreen(x, minY);
            var p2 = WorldToScreen(x, maxY);
            g.DrawLine(gridPen, p1, p2);
        }

        // Horizontal lines
        for (double y = Math.Ceiling(minY / gridStep) * gridStep; y <= maxY; y += gridStep)
        {
            var p1 = WorldToScreen(minX, y);
            var p2 = WorldToScreen(maxX, y);
            g.DrawLine(gridPen, p1, p2);
        }

        // === Icons ===
        const int iconSize = 28;
        const int half = iconSize / 2;

        foreach (var item in _items)
        {
            if (item.X < minX || item.X > maxX || item.Y < minY || item.Y > maxY)
                continue;

            var screen = WorldToScreen(item.X, item.Y);

            //if (_iconCache.TryGetValue(item.Type, out var bmp))
            //{
            //    g.DrawImage(bmp, screen.X - half, screen.Y - half, iconSize, iconSize);
            //}
            //else
            //{
                // Fallback shapes
                using var brush = new SolidBrush(Color.DodgerBlue);
                g.FillEllipse(brush, screen.X - 6, screen.Y - 6, 12, 12);
          //  }
        }
    }

    // ==================== Mouse Interaction ====================
    private bool _isDragging = false;
    private Point _lastMousePos;

    private void OnMouseWheel(object? sender, MouseEventArgs e)
    {
        double oldPpm = _pixelsPerMeter;
        double factor = e.Delta > 0 ? 1.25 : 0.8;

        _pixelsPerMeter *= factor;

        // Zoom toward mouse cursor
        var mouseWorld = ScreenToWorld(e.Location);
        _viewCenterX = mouseWorld.X;
        _viewCenterY = mouseWorld.Y;

        Invalidate();
    }

    private void OnMouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _isDragging = true;
            _lastMousePos = e.Location;
            Cursor = Cursors.SizeAll;
        }
    }

    private void OnMouseMove(object? sender, MouseEventArgs e)
    {
        if (_isDragging)
        {
            double dx = (e.X - _lastMousePos.X) / _pixelsPerMeter;
            double dy = (e.Y - _lastMousePos.Y) / _pixelsPerMeter;

            _viewCenterX -= dx;
            _viewCenterY -= dy;

            _lastMousePos = e.Location;
            Invalidate();
            return;
        }

        // Hover tooltip
        var hovered = HitTest(e.Location);
        if (hovered != _lastHovered)
        {
            if (hovered != null)
            {
                string text = hovered.Tooltip ??
                    $"X: {hovered.X:F1} m\nY: {hovered.Y:F1} m\nType: {hovered.Type}";

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

    private void OnMouseLeave(object? sender, EventArgs e)
    {
        _tooltip.Hide(this);
        _lastHovered = null;
    }

    // ==================== Cleanup ====================
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            //foreach (var bmp in _iconCache.Values)
            //    bmp?.Dispose();
            //_iconCache.Clear();
            _tooltip.Dispose();
        }
        base.Dispose(disposing);
    }
}
