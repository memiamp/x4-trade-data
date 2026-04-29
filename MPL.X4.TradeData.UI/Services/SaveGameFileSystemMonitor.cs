using Microsoft.Extensions.Logging;
using MPL.X4.TradeData.UI.Configuration;

namespace MPL.X4.TradeData.UI.Services;

/// <summary>
/// A class that implements a file system monitor for X4 save game files.
/// </summary>
/// <param name="fileConfiguration">An <see cref="IFileConfiguration"/> that is the file configuration for the service.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal sealed class SaveGameFileSystemMonitor(
                                                IFileConfiguration fileConfiguration,
                                                ILogger<SaveGameFileSystemMonitor> logger)
    : ISaveGameFileSystemMonitor
{
    private static readonly string[] ExcludedFilenames = ["temp", "tmp"];

    private bool _disposedValue;
    private bool _isRunning;
    private DateTimeOffset? _lastFileTime = DateTimeOffset.MinValue;
    private EventHandler<SaveGameFileUpdatedEventArgs>? _saveGameFileUpdatedEvent;
    private System.Threading.Timer? _timer;

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                ((ISaveGameFileSystemMonitor)this).Stop();

                _timer?.Dispose();
            }

            _disposedValue = true;
        }
    }

    private FileInfo? GetMostRecentSaveGameFile()
    {
        var fileAgeSeconds = 0 - (int)fileConfiguration.SaveGameFileAgeSeconds;
        var fileFilter = fileConfiguration.SaveGameFileFilter;
        var targetPath = fileConfiguration.SaveGameFilePath;
        var targetTime = DateTime.Now.AddSeconds(fileAgeSeconds);
        
        if (!Directory.Exists(targetPath))
        {
            logger.LogWarning("The configured save file path does not exist");
            return null;
        }

        var directory = new DirectoryInfo(targetPath);

        var mostRecentFile = directory
                                      .GetFiles(fileFilter, SearchOption.TopDirectoryOnly)
                                      .Where(x => !ExcludedFilenames.Any(y => x.FullName.Contains(y)))
                                      .Where(x => x.LastWriteTime < targetTime)
                                      .OrderByDescending(f => f.LastWriteTime)
                                      .FirstOrDefault();

        return mostRecentFile;
    }

    private bool OnSaveGameFileUpdated(string filePath)
    {
        var returnValue = true;

        if (_saveGameFileUpdatedEvent is not null)
        {
            var eventArgs = new SaveGameFileUpdatedEventArgs(filePath);

            _saveGameFileUpdatedEvent?.Invoke(this, eventArgs);

            returnValue = eventArgs.AutomaticallyRestartMonitor;
        }

        return returnValue;
    }

    private void StartTimer()
    {
        var intervalMs = fileConfiguration.CheckIntervalSeconds * 1000;

        if (_timer is null)
        {
            _timer = new System.Threading.Timer(Timer_ELapsed, null, intervalMs, Timeout.Infinite);
        }
        else
        {
            _timer?.Change(intervalMs, Timeout.Infinite);
        }
    }

    private void Timer_ELapsed(object? o)
    {
        if (_isRunning)
        {
            logger.LogDebug("{Name} timer elapsed", nameof(SaveGameFileSystemMonitor));

            try
            {
                var fileInfo = GetMostRecentSaveGameFile();
                if (fileInfo?.LastWriteTime > _lastFileTime)
                {
                    logger.LogDebug("New save game file detected: {SaveGameFilePath}", fileInfo.FullName);

                    _isRunning = OnSaveGameFileUpdated(fileInfo.FullName);

                    _lastFileTime = fileInfo.LastWriteTime;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An exception occurred whilst checking for recent save file updates");
            }

            if (_isRunning)
            {
                StartTimer();
            }
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    event EventHandler<SaveGameFileUpdatedEventArgs>? ISaveGameFileSystemMonitor.SaveGameFileUpdated
    {
        add => _saveGameFileUpdatedEvent += value;
        remove => _saveGameFileUpdatedEvent -= value;
    }

    void ISaveGameFileSystemMonitor.Start()
    {
        if (!_isRunning)
        {
            logger.LogInformation("Starting {Name}", nameof(SaveGameFileSystemMonitor));

            _isRunning = true;

            StartTimer();
        }
    }

    void ISaveGameFileSystemMonitor.Stop()
    {
        if (_isRunning)
        {
            _isRunning = false;

            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }

    bool ISaveGameFileSystemMonitor.IsRunning => _isRunning;
}
