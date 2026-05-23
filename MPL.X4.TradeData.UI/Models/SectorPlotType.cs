namespace MPL.X4.TradeData.UI.Models;

/// <summary>
/// An enumeration that defines the type of a sector plot item.
/// </summary>
internal enum SectorPlotType 
{ 
    /// <summary>
    /// The type is undefined.
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// The plot item is a collectable ammo drop.
    /// </summary>
    AmmoDrop,

    /// <summary>
    /// The plot item is a jump gate.
    /// </summary>
    JumpGate,

    /// <summary>
    /// The plot item is a lockbox.
    /// </summary>
    Lockbox,

    /// <summary>
    /// The plot item is a ship.
    /// </summary>
    Ship,

    /// <summary>
    /// The plot item is a station.
    /// </summary>
    Station,

    /// <summary>
    /// The plot item is a super highway.
    /// </summary>
    Superhighway,

    /// <summary>
    /// The plot item is a trans-orbital accelerator.
    /// </summary>
    TransorbitalAccelerator,

    /// <summary>
    /// The plot item is a collectable ware drop.
    /// </summary>
    WareDrop
}
