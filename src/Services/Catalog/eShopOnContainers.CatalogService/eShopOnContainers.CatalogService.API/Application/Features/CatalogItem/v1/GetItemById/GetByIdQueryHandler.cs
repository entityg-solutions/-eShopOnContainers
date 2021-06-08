using System;
using System.Threading;
using System.Threading.Tasks;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemById.Mappers;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemById.Requests;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemById.Responses;
using eShopOnContainers.CatalogService.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdDto, CatalogItemDetailDto>
    {
        private readonly CatalogContext _context;

        public GetByIdQueryHandler(CatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CatalogItemDetailDto> Handle(GetByIdDto request, CancellationToken cancellationToken)
        {
            var catalogItem = await _context.CatalogItems.SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken: cancellationToken);

            if (catalogItem == null)
            {
                throw new ArgumentNullException(nameof(catalogItem), $"Item with id {request.Id} not found.");
            }

            return CatalogItemDetailDtoMapper.Map(catalogItem);
        }
    }
}
