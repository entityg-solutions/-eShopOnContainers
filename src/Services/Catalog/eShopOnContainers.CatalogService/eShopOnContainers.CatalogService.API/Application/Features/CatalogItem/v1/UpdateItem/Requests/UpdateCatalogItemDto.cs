using MediatR;

namespace eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.UpdateItem.Requests
{
    public class UpdateCatalogItemDto : IRequest<int>
    {
        public int Id { get; set; }
        public int CatalogBrandId { get; set; }
        public int CatalogTypeId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string PictureFileName { get; set; }
        public decimal Price { get; set; }
        public int AvailableStock { get; set; }
        // Available stock at which we should reorder
        public int RestockThreshold { get; set; }
        // Maximum number of units that can be in-stock at any time (due to physicial/logistical constraints in warehouses)
        public int MaxStockThreshold { get; set; }
        public bool OnReorder { get; set; }
    }
}
