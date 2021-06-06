using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.UpdateItem.Requests;
using eShopOnContainers.CatalogService.API.Infrastructure.ESB;
using eShopOnContainers.CatalogService.API.Infrastructure.Persistence;
using eShopOnContainers.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.UpdateItem
{
    public class UpdateItemHandler : IRequestHandler<UpdateCatalogItemDto,int>
    {
        private readonly CatalogContext _context;
        private readonly IEventBusService _eventBusService;

        public UpdateItemHandler(CatalogContext context, IEventBusService eventBusService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _eventBusService = eventBusService ?? throw new ArgumentNullException(nameof(eventBusService));
        }

        public async Task<int> Handle(UpdateCatalogItemDto request, CancellationToken cancellationToken)
        {
            var catalogItem = await _context.CatalogItems.SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken: cancellationToken);

            if (catalogItem == null)
            {
                throw new ArgumentNullException(nameof(catalogItem), $"Item with id {request.Id} not found.");
            }

            decimal oldPrice = catalogItem.Price;

            bool raiseProductPriceChanged = catalogItem.Price != request.Price;

            await UpdateCatalogItem(catalogItem, request, cancellationToken);

            if (raiseProductPriceChanged)
            {
                var priceChangedEvent = new ProductPriceChangedEvent(catalogItem.Id, request.Price, oldPrice)
                {
                    MachineName = Environment.MachineName,
                    Originate = "eShopOnContainers.CatalogService.API"
                };

                await _eventBusService.PublishProductPriceChangedEvent(priceChangedEvent);
            }

            return catalogItem.Id;
        }

        private async Task UpdateCatalogItem(Infrastructure.Persistence.Entities.CatalogItem catalogItem, UpdateCatalogItemDto change, CancellationToken cancellationToken)
        {
            catalogItem.Name = change.Name;
            catalogItem.Description = change.Description;
            catalogItem.CatalogTypeId = change.CatalogTypeId;
            catalogItem.CatalogBrandId = change.CatalogBrandId;
            catalogItem.MaxStockThreshold = change.MaxStockThreshold;
            catalogItem.RestockThreshold = change.RestockThreshold;
            catalogItem.AvailableStock = change.AvailableStock;
            catalogItem.OnReorder = change.OnReorder;
            catalogItem.Price = change.Price;

            _context.CatalogItems.Update(catalogItem);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
