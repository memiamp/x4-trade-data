using Microsoft.Extensions.Logging;

namespace MPL.X4.TradeData.UI.Services.Logging;

/// <summary>
/// A class that implements a simple logger to the console.
/// </summary>
/// <param name="categoryName">A <see cref="string"/> containing the category of the logger entries.</param>
/// <param name="loggingQueue">An <see cref="ILoggingQueue"/> that is the logging queue to use.</param>
internal class SimpleConsoleLogger(
                                   string categoryName,
                                   ILoggingQueue loggingQueue)
    : ILogger
{
    IDisposable? ILogger.BeginScope<TState>(TState state)
        => null; // Scopes not supported here

    bool ILogger.IsEnabled(LogLevel logLevel)
        => true;

    void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!((ILogger)this).IsEnabled(logLevel) ||
            formatter == null)
            return;

        var message = formatter(state, exception);

        var logLine = $"[{DateTime.Now:HH:mm:ss}] {categoryName} - {logLevel}: {message}";

        if (exception != null)
            logLine += $"\nException: {exception}";

        loggingQueue.Enqueue(logLine);
    }
}
