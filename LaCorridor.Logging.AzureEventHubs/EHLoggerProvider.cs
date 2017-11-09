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
        private readonly LogLevel _logLevel;

        public EHLoggerProvider(LogLevel logLevel, string eventHubConnectionString)
        {
            _logLevel = logLevel;
            Arguments.IsNotNullOrEmpty(eventHubConnectionString, nameof(eventHubConnectionString));
            _client = EventHubClient.CreateFromConnectionString(eventHubConnectionString);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return this._loggers.GetOrAdd(categoryName, new Func<string, EHLogger>(CreateLoggerImplementation));
        }

        private EHLogger CreateLoggerImplementation(string categoryName)
        {
            return new EHLogger(_client, _logLevel, categoryName);
        }

        public void Dispose()
        {
            _client.Close();
        }
    }
}
