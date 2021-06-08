using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItems.Responses;
using eShopOnContainers.CatalogService.API.Models;
using eShopOnContainers.CatalogService.API.ViewModel;
using MediatR;

namespace eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItems.Requests
{
    public class GetAllItemDto : PaginatedQueryBaseDto, IRequest<PaginatedItemsDto<CatalogItemDto>>
    {
        public string Ids { get; set; }
    }
}
