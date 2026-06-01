namespace MPL.X4.TradeData.UI.Services;

/// <summary>
/// A class that implements event arguments for when a save game file have been updated.
/// </summary>
/// <param name="sectorName">A <see cref="string"/> containing the full path of the save game file.</param>
internal class SaveGameFileUpdatedEventArgs(
                                            string saveGameFilePath)
    : EventArgs
{
    /// <summary>
    /// Gets or sets an indication of whether to automatically restart the monitor.
    /// </summary>
    /// <remarks>This defaults to <see langword="true"/>.  If set to <see langword="false"/> then if monitor will only restart if manually invoked.</remarks>
    internal bool AutomaticallyRestartMonitor { get; set; } = true;

    /// <summary>
    /// Gets the full path of the save game file.
    /// </summary>
    internal string SaveGameFilePath => saveGameFilePath;
}
