using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeatherForcast.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        public static List<string> Summaries = new List<string>
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };




        [HttpPost("InsertSummary", Name = "InsertSummary")]
        public IActionResult InsertSummary(string summary)
        {
            if (Summaries.Contains(summary))
            {
                return StatusCode(409, "Summary already exists.");
            }

            Summaries.Add(summary);
            return StatusCode(200, "Summary successfully added.");

        }


        [HttpDelete(Name = "DeleteSummary")]
        public IActionResult DeleteSummary(string summary)
        {

            if (!Summaries.Contains(summary))
            {
                return StatusCode(400, "Summary doesnt exists. Cannot delet summary that does not exist.");
            }

            Summaries.RemoveAt(Summaries.IndexOf(summary));
            return StatusCode(200, "Summary successfully deleted.");
        }



    }
}
