using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements event arguments for a sector-based action.
/// </summary>
/// <param name="sector">A <see cref="ISectorModel"/> that is the sector.</param>
internal class SectorActionEventArgs(
                                     ISectorModel sector)
    : EventArgs
{
    /// <summary>
    /// Gets the sector code for the action.
    /// </summary>
    internal ISectorModel Sector => sector;
}
