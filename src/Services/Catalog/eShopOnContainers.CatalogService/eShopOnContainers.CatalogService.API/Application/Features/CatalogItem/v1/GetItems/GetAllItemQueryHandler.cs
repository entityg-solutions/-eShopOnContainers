using eShopOnContainers.CatalogService.API.Infrastructure.Persistence;
using eShopOnContainers.CatalogService.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItems.Mappers;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItems.Requests;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItems.Responses;

namespace eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItems
{
    public class GetAllItemQueryHandler : IRequestHandler<GetAllItemQuery, PaginatedItemsDto<CatalogItemDto>>
    {
        private readonly CatalogContext _context;

        public GetAllItemQueryHandler(CatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PaginatedItemsDto<CatalogItemDto>> Handle(GetAllItemQuery request, CancellationToken cancellationToken)
        {
            var query = _context.CatalogItems.AsQueryable();
            
            if (!string.IsNullOrEmpty(request.Ids))
            {
                var items = await GetItemsByIdsAsync(request.Ids);

                if (!items.Any())
                {
                    throw new Exception("ids value invalid. Must be comma-separated list of numbers");
                }

                return new PaginatedItemsDto<CatalogItemDto>(request.Page, request.PageSize, items.Count, items);
            }

            var totalItems = await _context.CatalogItems
                .LongCountAsync(cancellationToken: cancellationToken);

            var itemsOnPage = await _context.CatalogItems
                .OrderBy(c => c.Name)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => CatalogItemDtoMapper.Map(x))
                .ToListAsync(cancellationToken: cancellationToken);

            return new  PaginatedItemsDto<CatalogItemDto>(request.Page, request.PageSize, totalItems, itemsOnPage);
        }

        private async Task<List<CatalogItemDto>> GetItemsByIdsAsync(string ids)
        {
            List<(bool Ok, int Value)> numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x)).ToList();

            if (!numIds.All(nid => nid.Ok))
            {
                return new List<CatalogItemDto>();
            }

            IEnumerable<int> idsToSelect = numIds
                .Select(id => id.Value);

            List<CatalogItemDto> items = await _context.CatalogItems
                .Where(ci => idsToSelect.Contains(ci.Id))
                .Select(x => CatalogItemDtoMapper.Map(x))
                .ToListAsync();

            return items;
        }
    }
}