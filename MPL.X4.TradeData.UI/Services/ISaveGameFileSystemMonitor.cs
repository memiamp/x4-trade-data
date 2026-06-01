namespace MPL.X4.TradeData.UI.Services;

/// <summary>
/// An interface that defines the behaviour of a file system monitor for X4 save game files.
/// </summary>
internal interface ISaveGameFileSystemMonitor : IDisposable
{
    /// <summary>
    /// An event that is raised when a save game file has been updated.
    /// </summary>
    event EventHandler<SaveGameFileUpdatedEventArgs>? SaveGameFileUpdated;

    /// <summary>
    /// Starts the monitor.
    /// </summary>
    void Start();

    /// <summary>
    /// Stops the monitor.
    /// </summary>
    void Stop();

    /// <summary>
    /// Gets an indication of whether the monitor is running.
    /// </summary>
    bool IsRunning { get; }
}
