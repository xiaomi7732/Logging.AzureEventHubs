using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LaCorridor.Logging.AzureEventHubs
{
    public static class LoggerExtensions
    {
        public static ILoggerFactory AddEventHubLogger(this ILoggerFactory factory, string eventHubConnectionString, LogLevel minLogLevel = LogLevel.Warning)
        {
            factory.AddProvider(new EHLoggerProvider(minLogLevel, eventHubConnectionString));
            return factory;
        }

        public static ILoggingBuilder AddEventHub(this ILoggingBuilder builder, string eventHubConnectionString, LogLevel minLogLevel = LogLevel.Warning)
        {
            builder.Services.AddSingleton<ILoggerProvider, EHLoggerProvider>((serviceProvider) =>
            {
                return new EHLoggerProvider(minLogLevel, eventHubConnectionString);
            });

            return builder;
        }
    }
}
