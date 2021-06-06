using System;
using System.Threading;
using System.Threading.Tasks;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.CreateItem.Requests;
using eShopOnContainers.CatalogService.API.Infrastructure.Persistence;
using MediatR;

namespace eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.CreateItem
{
    public class CreateCatalogItemHandler : IRequestHandler<CreateCatalogItemDto, int>
    {
        private readonly CatalogContext _context;

        public CreateCatalogItemHandler(CatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> Handle(CreateCatalogItemDto request, CancellationToken cancellationToken)
        {
            var item = new Infrastructure.Persistence.Entities.CatalogItem
            {
                CatalogBrandId = request.CatalogBrandId,
                CatalogTypeId = request.CatalogTypeId,
                Description = request.Description,
                Name = request.Name,
                PictureFileName = request.PictureFileName,
                Price = request.Price
            };

            _context.CatalogItems.Add(item);

            await _context.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
