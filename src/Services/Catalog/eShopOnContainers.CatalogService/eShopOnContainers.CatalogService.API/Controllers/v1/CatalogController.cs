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
using eShopOnContainers.CatalogService.API.Application.Features.CatalogItem.v1.UpdateItem.Requests;

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
        [ProducesResponseType(typeof(PaginatedItemsDto<CatalogItemDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<CatalogItemDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetItemsAsync([FromQuery] GetAllItemQuery query)
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
    }
}

