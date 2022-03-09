using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

public class ColorConsoleLoggerConfiguration
{
    public Dictionary<LogLevel, ConsoleColor> LogLevels { get; set; } = new()
    {
        [LogLevel.Information] = ConsoleColor.Cyan,
        [LogLevel.Warning] = ConsoleColor.Yellow,
        [LogLevel.Error] = ConsoleColor.Red,
        [LogLevel.Critical] = ConsoleColor.White,
        [LogLevel.Debug] = ConsoleColor.Green,
        [LogLevel.Trace] = ConsoleColor.Gray,
    };
}

public class ColorConsoleLogger : ILogger
{
    private readonly string _name;
    private readonly Func<ColorConsoleLoggerConfiguration> _config;

    public ColorConsoleLogger(string name, Func<ColorConsoleLoggerConfiguration> config) =>
        (_name, _config) = (name, config);

    public IDisposable BeginScope<TState>(TState state) => default!;

    public bool IsEnabled(LogLevel logLevel) => _config().LogLevels.ContainsKey(logLevel);

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        ColorConsoleLoggerConfiguration config = _config();

        ConsoleColor originalColor = Console.ForegroundColor;

        Console.ForegroundColor = config.LogLevels[logLevel];
        Console.WriteLine($"{_name,2} : {logLevel,-12} : {formatter(state, exception)}");
        Console.ForegroundColor = originalColor;
    }
}

[ProviderAlias("ColorConsole")]
public class ColorConsoleLoggerProvider : ILoggerProvider
{
    private readonly Func<ColorConsoleLoggerConfiguration> _config;
    private ColorConsoleLogger _logger;

    public ColorConsoleLoggerProvider(IOptionsMonitor<ColorConsoleLoggerConfiguration> config) =>
        _config = () => config.CurrentValue;

    public ColorConsoleLoggerProvider(Func<ColorConsoleLoggerConfiguration> config) =>
        _config = config;

    public ILogger CreateLogger(string categoryName) =>
           _logger = new ColorConsoleLogger(categoryName, _config);

    public void Dispose()
    {
        _logger = null;
    }
}

public static class ColorConsoleLoggerExtensions
{
    public static ILoggingBuilder AddColorConsoleLogger(this ILoggingBuilder builder)
    {
        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, ColorConsoleLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions
            <ColorConsoleLoggerConfiguration, ColorConsoleLoggerProvider>(builder.Services);

        return builder;
    }

    public static ILoggingBuilder AddColorConsoleLogger(this ILoggingBuilder builder, Action<ColorConsoleLoggerConfiguration> configure)
    {
        builder.AddColorConsoleLogger();
        builder.Services.Configure(configure);

        return builder;
    }
}