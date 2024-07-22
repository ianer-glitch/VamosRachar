using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProtoServer.ProtoFiles;
using ProtoServer.ConnectionHelpers;

namespace Gateway.Controllers;

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

    [HttpGet]
    [Route("/wheather")]
    [Authorize]
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
    
    [HttpGet]
    [Route("/GetPayloadMessage")]
    public async  Task<string> GetPayloadMessage()
    {
        var request = new PayloadMessage
        {
            Message = "test grpc is working!!"
        };
        var client = MicroserviceConnection.GetIdentityClient<payloadExampleSerivce.payloadExampleSerivceClient>();
        
        var res = await client.GetPayloadMessageAsync(request);
            
        return res.Message;

    }

    

}
