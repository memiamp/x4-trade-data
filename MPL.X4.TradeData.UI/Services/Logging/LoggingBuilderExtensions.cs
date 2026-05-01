using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MPL.X4.TradeData.UI.Services.Logging;

/// <summary>
/// A class that implements extension methods for a <see cref="ILoggingBuilder"/>.
/// </summary>
internal static class LoggingBuilderExtensions
{
    /// <summary>
    /// Adds a simple console logger to the specified <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder">An <see cref="ILoggingBuilder"/> to add the logger to.</param>
    /// <param name="minLevel">A <see cref="LogLevel"/> indicating the minimum log level for the logger.</param>
    /// <returns>The passed-in <see cref="ILoggingBuilder"/> that can be used to chain calls.</returns>
    internal static ILoggingBuilder AddSimpleConsoleLogger(this ILoggingBuilder builder, LogLevel minLevel = LogLevel.Debug)
    {
        builder.Services.AddSingleton<ILoggerProvider>(new SimpleConsoleLoggerProvider(minLevel));

        return builder;
    }
}
