using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItems.Responses;

namespace eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItems.Mappers
{
    public static class CatalogItemDtoMapper
    {
        public static CatalogItemDto Map(Infrastructure.Persistence.Entities.CatalogItem item)
        {
            return new CatalogItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                CatalogTypeId = item.CatalogTypeId,
                AvailableStock = item.AvailableStock,
                CatalogBrandId = item.CatalogBrandId,
                OnReorder = item.OnReorder,
                Price = item.Price,
                MaxStockThreshold = item.MaxStockThreshold,
                RestockThreshold = item.RestockThreshold,
                PictureFileName = item.PictureFileName,
                PictureUri = item.PictureUri
            };
        }
    }
}
