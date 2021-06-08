using eShopOnContainers.Events;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace eShopOnContainers.CatalogService.API.Infrastructure.ESB
{
    public class AzureServiceBusService : IEventBusService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public AzureServiceBusService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public Task PublishProductPriceChangedEvent(ProductPriceChangedEvent @event)
        {
            return _publishEndpoint.Publish(@event);
        }
    }
}
