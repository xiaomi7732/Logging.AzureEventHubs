using System;
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
    }
}
