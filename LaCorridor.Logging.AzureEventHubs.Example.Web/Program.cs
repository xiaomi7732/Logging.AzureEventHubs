using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace LaCorridor.Logging.AzureEventHubs.Example.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.AddEventHub(
                        "Endpoint=sb://saars-sb-eh-test02-ns.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=DvQHUtZPFuBh02GmBbmq29hh9Cce452XX8BazuhTPsQ=;EntityPath=saars-sb-eh-test02",
                        LogLevel.Debug);
                })
                .UseStartup<Startup>()
                .Build();
    }
}
