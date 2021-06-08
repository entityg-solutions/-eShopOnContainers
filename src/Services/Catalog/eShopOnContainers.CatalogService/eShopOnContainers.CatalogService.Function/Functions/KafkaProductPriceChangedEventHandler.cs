using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;

namespace eShopOnContainers.CatalogService.Function.Functions
{
    public class KafkaProductPriceChangedEventHandler
    {
        // KafkaTrigger sample 
        // Consume the message from "topic" on the LocalBroker.
        // Add `BrokerList` and `Password` to the local.settings.json
        // For EventHubs
        // "BrokerList": "{EVENT_HUBS_NAMESPACE}.servicebus.windows.net:9093"
        // "Password":"{EVENT_HUBS_CONNECTION_STRING}
        [FunctionName("KafkaProductPriceChangedEventHandler")]
        public static void Run(
            [KafkaTrigger("13.76.4.84:9092",
                          "ProductPriceChangedEvent",
                          Protocol = BrokerProtocol.NotSet,
                          AuthenticationMode = BrokerAuthenticationMode.NotSet,
                          ConsumerGroup = "AzureFunction.ProductPriceChangedEvent.Local")] KafkaEventData<string>[] events, ILogger log)
        {
            foreach (KafkaEventData<string> eventData in events)
            {
                log.LogInformation($"C# Kafka trigger function processed a message: {eventData.Value}");
            }
        }
    }
}
