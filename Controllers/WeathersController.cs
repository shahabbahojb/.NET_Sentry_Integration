using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SampleApp_Review.Controllers;

[ApiController]
[Route("[controller]")]
public class WeathersController : ControllerBase
{
    [HttpGet("/get-weather-forecast")]
    public Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        IEnumerable<WeatherForecast> forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        
        SentrySdk.CaptureException(new NullReferenceException("This is NullReferenceException"));
        
        return Task.FromResult<ActionResult<IEnumerable<WeatherForecast>>>(Ok(forecast));
    }
}