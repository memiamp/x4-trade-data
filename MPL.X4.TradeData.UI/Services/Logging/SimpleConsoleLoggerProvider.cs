using Microsoft.Extensions.Logging;

namespace MPL.X4.TradeData.UI.Services.Logging;

/// <summary>
/// A class that implements a logger provider for a <see cref="SimpleConsoleLogger"/>.
/// </summary>
/// <param name="loggingQueue">An <see cref="ILoggingQueue"/> that is the logging queue to use.</param>
internal class SimpleConsoleLoggerProvider(
                                           ILoggingQueue loggingQueue)
    : ILoggerProvider
{
    public void Dispose()
    {
        // Nothing to do here as no managed resources
    }

    ILogger ILoggerProvider.CreateLogger(string categoryName)
        => new SimpleConsoleLogger(categoryName, loggingQueue);
}
