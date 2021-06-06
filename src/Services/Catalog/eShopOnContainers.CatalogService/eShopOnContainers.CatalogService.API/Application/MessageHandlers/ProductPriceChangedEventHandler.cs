using System.Threading.Tasks;
using eShopOnContainers.Events;
using MassTransit;

namespace eShopOnContainers.CatalogService.API.Application.MessageHandlers
{
    public class ProductPriceChangedEventHandler : IConsumer<ProductPriceChangedEvent>
    {
        public Task Consume(ConsumeContext<ProductPriceChangedEvent> context)
        {
            var message = context.Message;
            return Task.CompletedTask;
        }
    }
}
