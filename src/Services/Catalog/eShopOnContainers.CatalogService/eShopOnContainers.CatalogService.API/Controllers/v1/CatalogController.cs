using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItems.Requests;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItems.Responses;
using eShopOnContainers.CatalogService.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.CreateItem.Requests;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemById.Requests;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemById.Responses;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.GetItemsWithName.Request;
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.UpdateItem.Requests;
using eShopOnContainers.CatalogService.API.Infrastructure.Persistence.Entities;
using eShopOnContainers.CatalogService.API.ViewModel;

namespace eShopOnContainers.CatalogService.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CatalogController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(PaginatedItemsDto<CatalogItemDetailDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<CatalogItemDetailDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetItemsAsync([FromQuery] GetAllItemDto query)
        {
            return Ok(await  _mediator.Send(query));
        }

        //POST api/v1/[controller]/items
        [Route("items")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateItemAsync([FromBody] CreateCatalogItemDto command)
        {
            return Ok(await _mediator.Send(command));
        }

        //PUT api/v1/[controller]/items
        [Route("items")]
        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateItemAsync([FromBody] UpdateCatalogItemDto command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("items/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CatalogItemDetailDto>> ItemByIdAsync(int id)
        {
            return Ok(await _mediator.Send(new GetByIdDto(id)));
        }

        // GET api/v1/[controller]/items/withname/samplename[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items/withname/{name:minlength(1)}")]
        [ProducesResponseType(typeof(PaginatedItemsDto<CatalogItemDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsDto<CatalogItemDto>>> ItemsWithNameAsync(string name,[FromQuery] PaginatedQueryBaseDto query)
        {
            return Ok(await _mediator.Send(new GetItemsWithNameDto(query.Page, query.PageSize, name)));
        }

    }
}

