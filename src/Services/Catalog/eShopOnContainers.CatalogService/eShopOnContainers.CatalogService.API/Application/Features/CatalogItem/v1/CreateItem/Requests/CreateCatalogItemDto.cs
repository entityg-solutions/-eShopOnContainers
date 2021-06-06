using MediatR;

namespace eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.CreateItem.Requests
{
    public class CreateCatalogItemDto : IRequest<int>
    {
        public int CatalogBrandId { get; set; }
        public int CatalogTypeId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string PictureFileName { get; set; }
        public decimal Price { get; set; }
    }
}
