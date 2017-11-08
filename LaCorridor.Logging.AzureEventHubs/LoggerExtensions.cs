using System;
using Microsoft.Extensions.Logging;

namespace LaCorridor.Logging.AzureEventHubs
{
    public static class LoggerExtensions
    {
        public static ILoggerFactory AddEventHubLogger(this ILoggerFactory factory, string eventHubConnectionString, Func<string, LogLevel> filter = null)
        {
            factory.AddProvider(new EHLoggerProvider(filter, eventHubConnectionString));
            return factory;
        }
    }
}
