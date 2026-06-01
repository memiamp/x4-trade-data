using Microsoft.Extensions.Logging;

namespace MPL.X4.TradeData.UI.Services.Logging;

/// <summary>
/// A class that implements a logger provider for a <see cref="SimpleConsoleLogger"/>.
/// </summary>
/// <param name="minLevel">A <see cref="LogLevel"/> indicating the minimum level the logger should output.</param>
internal class SimpleConsoleLoggerProvider(
                                           LogLevel minLevel = LogLevel.Debug)
    : ILoggerProvider
{
    public void Dispose()
    {
        // Nothing to do here as no managed resources
    }

    ILogger ILoggerProvider.CreateLogger(string categoryName)
        => new SimpleConsoleLogger(categoryName, minLevel);
}
