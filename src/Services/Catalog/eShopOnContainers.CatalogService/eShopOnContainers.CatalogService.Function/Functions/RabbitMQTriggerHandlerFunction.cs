using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace eShopOnContainers.CatalogService.Function.Functions
{
    public static class RabbitMQTriggerHandlerFunction
    {
        [FunctionName("RabbitMQTriggerHandlerFunction")]
        public static void Run([RabbitMQTrigger("ProductPriceChangedEvent", ConnectionStringSetting = "RabbitMQ")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
