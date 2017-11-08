using System;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Logging;

namespace LaCorridor.Logging.AzureEventHubs
{
    public class EHLogger : ILogger
    {
        private readonly LogLevel _logLevel;
        private readonly EventHubClient _eventHubClient;
        private readonly string _category;

        public EHLogger(EventHubClient eventHubClient, LogLevel logLevel, string category)
        {
            _logLevel = logLevel;
            _eventHubClient = eventHubClient;
            _category = category;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            // Not supported yet.
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _logLevel;
        }

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            string message = formatter(state, exception);

            LogEntry logEntry = new LogEntry(logLevel, eventId, message, _category);
            EventData eventData = logEntry.ToEventData();

            if (eventData != null)
            {
                await _eventHubClient.SendAsync(eventData);
            }
        }
    }
}
