using eShopOnContainers.Events;
using MassTransit.KafkaIntegration;

namespace eShopOnContainers.CatalogService.API.Infrastructure.ESB
{
    public interface ITopicProducerFactory
    {
        ITopicProducer<ProductPriceChangedEvent> ProductPriceChangedEventProducer { get; }
    }
}
