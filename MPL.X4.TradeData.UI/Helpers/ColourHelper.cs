namespace MPL.X4.TradeData.UI.Helpers;

/// <summary>
/// A class that provides helper functions for colours.
/// </summary>
internal static class ColourHelper
{
    private static Color ColorFromHsl(double hue, double saturation, double lightness)
    {
        double c = (1 - Math.Abs(2 * lightness - 1)) * saturation;
        double x = c * (1 - Math.Abs((hue / 60) % 2 - 1));
        double m = lightness - c / 2;

        double r1 = 0, g1 = 0, b1 = 0;

        if (hue < 60) { r1 = c; g1 = x; }
        else if (hue < 120) { r1 = x; g1 = c; }
        else if (hue < 180) { g1 = c; b1 = x; }
        else if (hue < 240) { g1 = x; b1 = c; }
        else if (hue < 300) { r1 = x; b1 = c; }
        else { r1 = c; b1 = x; }

        return Color.FromArgb(255,
            (int)((r1 + m) * 255),
            (int)((g1 + m) * 255),
            (int)((b1 + m) * 255));
    }

    internal static Color GetAverageGradientColour(double value, double average, double maxDeviationPercent = 0.25)
    {
        if (average == 0)
            return Color.LightGray;

        double deviation = (value - average) / average;
        double t = Math.Clamp(deviation / maxDeviationPercent, -1.0, 1.0);

        double hue = 60 + (60 * t);

        double saturation = 0.95;
        double lightness = 0.52;

        return ColorFromHsl(hue, saturation, lightness);
    }
}
