using Microsoft.AspNetCore.Mvc;

namespace WeatherForcast.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new List<string>
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Count)]
            })
            .ToArray();
        }


        [HttpPost(Name = "InsertSummary")]
        public IActionResult InsertSummary(string summary)
        {
            if (Summaries.Contains(summary))
            {
                return StatusCode(409, "Summary already exists.");
            }
           
            Summaries.Add(summary);
            return StatusCode(200, "Summary successfully added.");
            
        }

    }
}
