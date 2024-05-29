using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace WeatherForcast.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
    
        private static List<WeatherForecast> forecasts = new List<WeatherForecast>();

        [HttpPost("GenerateWeatherForecast", Name ="GenerateWeatherForecast")]
        public IActionResult GenerateWeatherForecast()
        {
            for (int i = 0; i < 10; i++) 
            {
               DateOnly date = DateOnly.FromDateTime(DateTime.Now.AddDays(i));
               int temperatureC = Random.Shared.Next(-20, 55);
               string summary = SummaryController.Summaries[Random.Shared.Next(SummaryController.Summaries.Count)];

                forecasts.Add(new WeatherForecast(date, temperatureC, summary));
            }

            return Ok("Weather forcasts generated successffully.");
            
        }

        [HttpGet(Name = "GetWeatherForecasts")]
        public IActionResult GetWeatherForecasts()
        {
            List<string> weatherForcasts = new List<string>();
            foreach(var forecast in forecasts)
            {
                weatherForcasts.Add($"Date:{forecast.Date}, Temperature: {forecast.TemperatureC}, Summary: {forecast.Summary}");
            }
            return Ok(weatherForcasts.ToArray());
        }


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        




       

    }
}


