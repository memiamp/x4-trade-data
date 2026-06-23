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
        internal static readonly Color Important = Color.Green;
        internal static readonly Color ImportantLess = Color.LightGreen;
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

    /// <summary>
    /// A class containing sector-related constants.
    /// </summary>
    internal static class Sectors
    {
        internal static readonly IEnumerable<string> HyperloopSectors =
        [
            "Argon Prime",
            "Bright Promise",
            "Hatikvah's Choice I",
            "Holy Vision",
            "Pious Mists II",
            "Pontifex's Claim",
            "Profit Center Alpha",
            "Second Contact II Flashpoint",
            "Silent Witness I",
            "Trinity Sanctum III",
            "True Sight",
            "Unholy Retribution"
        ];

        internal static string Unknown = "Unknown Sector";
    }

    /// <summary>
    /// A class containing ship class constants.
    /// </summary>
    internal static class ShipClass
    {
        internal static string ExtraLarge = "Extra large";
        internal static string ExtraSmall = "Extra small";
        internal static string Large = "Large";
        internal static string Medium = "Medium";
        internal static string Small = "Small";
    }

    /// <summary>
    /// A class containing ship modification constants.
    /// </summary>
    internal static class ShipModifications
    {
        internal const string ModificationSymbol = "〉";

        internal const string QualityBasic = ModificationSymbol;
        internal const string QualityEnhanced = $"{ModificationSymbol}{ModificationSymbol}";
        internal const string QualityExceptional = $"{ModificationSymbol}{ModificationSymbol}{ModificationSymbol}";
        internal const string QualityPaint = "P";

        internal static string Unknown = "Unknown";
    }

    /// <summary>
    /// A class containing special item type constants.
    /// </summary>
    internal static class SpecialItemTypes
    {
        internal static string BuildStorage = "Build Storage";
        internal static string Lockbox = "Lockbox";
        internal static string ShipExtraLarge = "Extra large ship";
        internal static string ShipExtraSmall = "Extra small ship";
        internal static string ShipLarge = "Large ship";
        internal static string ShipMedium = "Medium ship";
        internal static string ShipSmall = "Small ship";
    }
}
