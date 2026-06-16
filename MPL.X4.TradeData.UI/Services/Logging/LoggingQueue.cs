using System.Collections.Concurrent;

namespace MPL.X4.TradeData.UI.Services.Logging;

/// <summary>
/// A class that implements a logging queue.
/// </summary>
internal class LoggingQueue : ILoggingQueue
{
    private readonly ConcurrentQueue<string> _logQueue = new();

    void ILoggingQueue.Enqueue(string? item)
    {
        if (!string.IsNullOrWhiteSpace(item))
        {
            _logQueue.Enqueue(item);
        }
    }

    bool ILoggingQueue.TryDequeue(out string? result)
        => _logQueue.TryDequeue(out result);
}
