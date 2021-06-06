using eShopOnContainers.Events;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace eShopOnContainers.CatalogService.Function.Functions
{
    public static class ProductPriceChangedEventHandlerFunction
    {
        [FunctionName("ProductPriceChangedEventHandlerFunction")]
        public static void Run([ServiceBusTrigger(nameof(ProductPriceChangedEvent), "eShopOnContainers.CatalogService.Function", Connection = "AzureServiceBus")] string messageContent, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {messageContent}");

            var message = JsonConvert.DeserializeObject<ProductPriceChangedEvent>(messageContent); 
        }
    }
}
