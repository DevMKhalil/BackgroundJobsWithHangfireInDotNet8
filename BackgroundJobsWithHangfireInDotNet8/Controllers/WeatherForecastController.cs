using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundJobsWithHangfireInDotNet8.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            BackgroundJob.Enqueue(() => SendMessage("mKhalil@elm.sa"));

            BackgroundJob.Schedule(() => SendMessage("MohamedKhalil@ntgClarity.com"),TimeSpan.FromSeconds(30));

            RecurringJob.AddOrUpdate(() => SendMessage("dev.MohammedKhalil@gmail.com"), Cron.Minutely);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task SendMessage(string email)
        {
            Console.WriteLine($"Email Sent: {email} Date: {DateTime.Now}");
        }
    }
}
