using eShopOnContainers.CatalogService.API.Infrastructure.ESB;
using eShopOnContainers.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit.KafkaIntegration;

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

        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
            IEventBusService eventBusService)
        {
            _logger = logger;
            _eventBusService = eventBusService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string text)
        {
            
            //await _producer.Produce(new ProductPriceChangedEvent(1, 3, 3)
            //{

            //});

            try
            {
                await _eventBusService.PublishProductPriceChangedEvent(new ProductPriceChangedEvent(100, 25, 56)
                {
                    MachineName = Environment.MachineName,
                    Originate = text
                });

                var rng = new Random();
                var res= Enumerable.Range(1, 5).Select(index => new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    })
                    .ToArray();
                return Ok(res);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            
        }
    }
}
