using eShopOnContainers.Events.Base;

namespace eShopOnContainers.Events
{
    public class ProductPriceChangedEvent : BaseEvent
    {
        public int ProductId { get; private set; }

        public decimal NewPrice { get; private set; }

        public decimal OldPrice { get; private set; }

        public ProductPriceChangedEvent(int productId, decimal newPrice, decimal oldPrice)
        {
            ProductId = productId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}
