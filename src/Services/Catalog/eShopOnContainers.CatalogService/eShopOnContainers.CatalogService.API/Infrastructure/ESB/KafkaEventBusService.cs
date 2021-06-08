using eShopOnContainers.Events;
using System;
using System.Threading.Tasks;

namespace eShopOnContainers.CatalogService.API.Infrastructure.ESB
{
    public class KafkaEventBusService : IEventBusService
    {
        private readonly ITopicProducerFactory _topicProducerFactory;

        public KafkaEventBusService(ITopicProducerFactory topicProducerFactory)
        {
            _topicProducerFactory = topicProducerFactory ?? throw new ArgumentNullException(nameof(topicProducerFactory));
        }

        public Task PublishProductPriceChangedEvent(ProductPriceChangedEvent @event)
        {
            var producer = _topicProducerFactory.ProductPriceChangedEventProducer;
            var result =  producer.Produce(@event);
            return result;
        }
    }
}
