using eShopOnContainers.CatalogService.API.Infrastructure.ESB;
using eShopOnContainers.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eShopOnContainers.CatalogService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEventBusService _eventBusService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEventBusService eventBusService)
        {
            _logger = logger;
            _eventBusService = eventBusService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _eventBusService.PublishProductPriceChangedEvent(new ProductPriceChangedEvent(100, 25, 56)
            {
                MachineName = Environment.MachineName,
                Originate = "eShopOnContainers.CatalogService.API"
            }).GetAwaiter();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
