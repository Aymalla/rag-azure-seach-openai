using Microsoft.AspNetCore.Mvc;
using Shared.Models;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly ILogger<ApiController> _logger;

    public ApiController(ILogger<ApiController> logger)
    {
        _logger = logger;
    }

    [HttpGet("weather")]
    public IResult Weather()
    {
        var result = new List<WeatherForecast>
        {
               new WeatherForecast
               {
                Date = DateTime.Now.AddDays(1),
                TemperatureC = 1,
                Summary = "Freezing"
            },
               new WeatherForecast
               {
                 Date = DateTime.Now.AddDays(2),
                 TemperatureC = 14,
                 Summary = "Bracing"
                },
                new WeatherForecast
                {
                 Date = DateTime.Now.AddDays(3),
                 TemperatureC = -13,
                 Summary = "Freezing"
                },
                new WeatherForecast
                {
                 Date = DateTime.Now.AddDays(4),
                 TemperatureC = -16,
                 Summary = "Balmy"
                },
                new WeatherForecast
                {
                 Date = DateTime.Now.AddDays(5),
                 TemperatureC = -2,
                 Summary = "Chilly"
                }
        };

        return TypedResults.Ok(result);
    }
}