using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MPL.X4.TradeData.UI;

public class SimpleConsoleLogger : ILogger
{
    private readonly string _categoryName;
    private readonly LogLevel _minLevel;

    public SimpleConsoleLogger(string categoryName, LogLevel minLevel = LogLevel.Debug)
    {
        _categoryName = categoryName;
        _minLevel = minLevel;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null; // No scope support needed for simple case

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _minLevel;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel) || formatter == null)
            return;

        var message = formatter(state, exception);

        var logLine = $"[{DateTime.Now:HH:mm:ss}] {_categoryName} - {logLevel}: {message}";

        if (exception != null)
            logLine += $"\nException: {exception}";

        Console.WriteLine(logLine);
    }
}

public class SimpleConsoleLoggerProvider : ILoggerProvider
{
    private readonly LogLevel _minLevel;

    public SimpleConsoleLoggerProvider(LogLevel minLevel = LogLevel.Debug)
    {
        _minLevel = minLevel;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new SimpleConsoleLogger(categoryName, _minLevel);
    }

    public void Dispose() { }
}

public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder AddSimpleConsoleLogger(
        this ILoggingBuilder builder,
        LogLevel minLevel = LogLevel.Debug)
    {
        builder.Services.AddSingleton<ILoggerProvider>(
            new SimpleConsoleLoggerProvider(minLevel));

        return builder;
    }
}