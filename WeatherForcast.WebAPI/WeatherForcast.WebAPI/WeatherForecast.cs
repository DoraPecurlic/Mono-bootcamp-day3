namespace WeatherForcast.WebAPI
{
    //MODEL
    public class WeatherForecast
    {
        public WeatherForecast(DateOnly date, int temperatureC, string summary) 
        { 
            this.Date = date;      
            this.TemperatureC = temperatureC;
            this.Summary = summary;
        }
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
