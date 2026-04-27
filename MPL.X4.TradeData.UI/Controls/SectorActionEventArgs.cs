namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements event arguments for a sector-based action.
/// </summary>
/// <param name="sectorName">A <see cref="string"/> containing the sector code.</param>
internal class SectorActionEventArgs(
                                     string sectorCode)
    : EventArgs
{
    /// <summary>
    /// Gets the sector code for the action.
    /// </summary>
    internal string SectorCode => sectorCode;
}
