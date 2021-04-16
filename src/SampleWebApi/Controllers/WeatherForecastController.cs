using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Logging;

namespace IGT.Swashbuckle.OData.SampleWebApi.Controllers
{
    [ApiController]
    [Route("weather")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class WeatherForecastController : ODataController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IEnumerable<WeatherForecast> _Weather;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            var rng = new Random();
            _logger = logger;

            _Weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet, EnableQuery]
        [Route("{summary}")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult Get(string? summary = null)
        {
            
            return summary is null ? Ok(_Weather) : Ok(_Weather.Where(w => w.Summary == summary));
        }
    }
}
