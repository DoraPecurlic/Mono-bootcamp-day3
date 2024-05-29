using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Net;

namespace WeatherForcast.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
    
        private static List<WeatherForecast> forecasts = new List<WeatherForecast>();

        [HttpPost("GenerateWeatherForecast", Name ="GenerateWeatherForecast")]
        public async Task<HttpResponseMessage> GenerateWeatherForecast()
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    DateOnly date = DateOnly.FromDateTime(DateTime.Now.AddDays(i));
                    int temperatureC = Random.Shared.Next(-20, 55);
                    string summary = SummaryController.Summaries[Random.Shared.Next(SummaryController.Summaries.Count)];

                    forecasts.Add(new WeatherForecast(date, temperatureC, summary));
                }

                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            }
            catch
            {
                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            
            
        }

        [HttpGet(Name = "GetWeatherForecasts")]
        public IActionResult GetWeatherForecasts()
        {
            try {
                List<string> weatherForcasts = new List<string>();
                foreach (var forecast in forecasts)
                {
                    weatherForcasts.Add($"Date:{forecast.Date}, Temperature: {forecast.TemperatureC}, Summary: {forecast.Summary}");
                }
                return Ok(weatherForcasts.ToArray());
            }catch{
                return BadRequest();
            }
            
        }


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        [HttpDelete(Name = "DeleteWeatherOnDate")]
        public async Task<HttpResponseMessage> DeleteWeatherOnDate([FromQuery] string date)
        {
            try
            {
                int indexToRemove = -1;
                DateOnly deleteDate = DateOnly.Parse(date);

                for (int i = 0; i < forecasts.Count ;  i++)
                {
                    if(forecasts[i].Date == deleteDate)
                    {
                        indexToRemove = i;
                        break;
                    }
                }

                if (indexToRemove != -1) { 
                    forecasts.RemoveAt(indexToRemove);

                    return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
                }

                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));

            }
            catch (HttpRequestException exception)
            {
                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }


        }



    }
}


