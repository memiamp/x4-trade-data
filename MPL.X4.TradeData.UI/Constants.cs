namespace MPL.X4.TradeData.UI;

/// <summary>
/// A class containing constant values for the UI.
/// </summary>
internal static class Constants
{
    /// <summary>
    /// A class containing colour-related constants.
    /// </summary>
    internal static class Colours
    {
        internal static readonly Color AbandonedShip = Color.White;
        internal static readonly Color Collectable = Color.FromArgb(0, 149, 255);
        internal static readonly Color Gate = Color.LightGray;
        internal static readonly Color Unset = SystemColors.Window;
        internal static readonly Color Wreck = Color.Gray;
    }

    /// <summary>
    /// A class containing owner-related constants.
    /// </summary>
    internal static class Owner
    {
        internal static string Unowned = "None";
    }
}
