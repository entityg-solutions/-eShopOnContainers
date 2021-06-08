using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemById.Responses;
using MediatR;

namespace eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemById.Requests
{
    public class GetByIdDto : IRequest<CatalogItemDetailDto>
    {
        public GetByIdDto(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
