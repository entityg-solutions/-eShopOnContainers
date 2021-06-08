using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemById.Responses;

namespace eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemById.Mappers
{
    public static class CatalogItemDetailDtoMapper
    {
        public static CatalogItemDetailDto Map(Infrastructure.Persistence.Entities.CatalogItem item)
        {
            return new CatalogItemDetailDto
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
