using eShopOnContainers.Events;
using System.Threading.Tasks;

namespace eShopOnContainers.CatalogService.API.Infrastructure.ESB
{
    public interface IEventBusService
    {
        Task PublishProductPriceChangedEvent(ProductPriceChangedEvent @event);
    }
}
