using Microsoft.Extensions.DependencyInjection;
using System;
using eShopOnContainers.Events;
using MassTransit.KafkaIntegration;

namespace eShopOnContainers.CatalogService.API.Infrastructure.ESB
{
    public class TopicProducerFactory : ITopicProducerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public TopicProducerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ITopicProducer<ProductPriceChangedEvent> ProductPriceChangedEventProducer => _serviceProvider.GetRequiredService<ITopicProducer<ProductPriceChangedEvent>>();
    }
}
