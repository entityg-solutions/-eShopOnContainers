using eShopOnContainers.Events.Core;

namespace eShopOnContainers.Events
{
    public class ProductPriceChangedEvent : IntegrationEvent
    {
        public int ProductId { get; private set; }

        public decimal NewPrice { get; private set; }

        public decimal OldPrice { get; private set; }

        public ProductPriceChangedEvent(string correlationId, int productId, decimal newPrice, decimal oldPrice) : base(correlationId)
        {
            ProductId = productId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}
