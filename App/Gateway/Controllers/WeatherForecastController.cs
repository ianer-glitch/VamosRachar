using Microsoft.AspNetCore.Mvc;
using Identity;
using Identity.UseCases.UserCase;
using Identity.Models;
namespace Gateway.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly UseCaseUser _userCase;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,UseCaseUser userCase)
    {
        _logger = logger;
        _userCase = userCase;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet(Name = "CreateUserTest")]
    public async  Task<ActionResult<User>> CreateUserTest()
    {
        var userA = new User{
            Name = "Ian"
        };

        var result = await _userCase.CreateUser(userA);
        return Ok(result);
    }

}
