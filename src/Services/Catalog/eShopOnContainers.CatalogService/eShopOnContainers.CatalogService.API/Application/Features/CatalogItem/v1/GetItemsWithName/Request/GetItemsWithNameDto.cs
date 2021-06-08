using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItems.Responses;
using eShopOnContainers.CatalogService.API.Models;
using eShopOnContainers.CatalogService.API.ViewModel;
using MediatR;

namespace eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemsWithName.Request
{
    public class GetItemsWithNameDto : PaginatedQueryBaseDto, IRequest<PaginatedItemsDto<CatalogItemDto>>
    {
        public GetItemsWithNameDto(int page, int pageSize, string name)
        {
            this.Page = page;
            this.PageSize = pageSize;
            this.Name = name;
        }
        public string Name { get; private set; }
    }
}
