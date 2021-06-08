using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItems.Responses;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemsWithName.Request;
using eShopOnContainers.CatalogService.API.Infrastructure.Persistence;
using eShopOnContainers.CatalogService.API.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItems.Mappers;
using Microsoft.EntityFrameworkCore;

namespace eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemsWithName
{
    public class GetItemsWithNameQueryHandler : IRequestHandler<GetItemsWithNameDto, PaginatedItemsDto<CatalogItemDto>>
    {
        private readonly CatalogContext _context;


        public GetItemsWithNameQueryHandler(CatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PaginatedItemsDto<CatalogItemDto>> Handle(GetItemsWithNameDto request, CancellationToken cancellationToken)
        {

            var query = _context.CatalogItems.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));

            }
            
            long totalItems = await query.LongCountAsync(cancellationToken: cancellationToken);

            var items = await query.OrderBy(c => c.Name)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => CatalogItemDtoMapper.Map(x))
                .ToListAsync(cancellationToken: cancellationToken);

            return new PaginatedItemsDto<CatalogItemDto>(request.Page, request.PageSize, totalItems, items);
        }
    }
}
