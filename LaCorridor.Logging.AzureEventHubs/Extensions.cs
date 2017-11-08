using System.Text;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;

namespace LaCorridor.Logging.AzureEventHubs
{
    internal static class Extensions
    {
        public static EventData ToEventData(this LogEntry logEntry)
        {
            string serialized = JsonConvert.SerializeObject(logEntry);
            EventData eventData = new EventData(Encoding.UTF8.GetBytes(serialized));
            return eventData;
        }
    }
}
