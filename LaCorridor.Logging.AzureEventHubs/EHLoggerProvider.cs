using System;
using System.Collections.Concurrent;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Logging;

namespace LaCorridor.Logging.AzureEventHubs
{
    public class EHLoggerProvider : ILoggerProvider
    {
        private readonly EventHubClient _client;
        private readonly ConcurrentDictionary<string, EHLogger> _loggers = new ConcurrentDictionary<string, EHLogger>();
        private readonly Func<string, LogLevel> _filter;

        public EHLoggerProvider(Func<string, LogLevel> filter, string eventHubConnectionString)
        {
            _filter = filter;
            Arguments.IsNotNullOrEmpty(eventHubConnectionString, nameof(eventHubConnectionString));
            _client = EventHubClient.CreateFromConnectionString(eventHubConnectionString);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return this._loggers.GetOrAdd(categoryName, new Func<string, EHLogger>(CreateLoggerImplementation));
        }

        private EHLogger CreateLoggerImplementation(string categoryName)
        {
            Func<string, LogLevel> targetFilter = _filter;
            if (targetFilter == null)
            {
                targetFilter = new Func<string, LogLevel>(name => LogLevel.Warning);
            }
            return new EHLogger(_client, targetFilter(categoryName), categoryName);
        }

        public void Dispose()
        {
            _client.Close();
        }
    }
}
