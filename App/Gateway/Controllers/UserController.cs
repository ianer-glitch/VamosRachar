using Microsoft.AspNetCore.Mvc;
using ProtoServer.ConnectionHelpers;
using ProtoServer.ProtoFiles;

namespace Gateway.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    [HttpPost]
    [Route("CreateUser")]
    public async Task<ActionResult<PCreateUser>> CreateUser([FromBody] PCreateUser request)
    {
        try
        {
            var client = MicroserviceConnection.GetIdentityClient<UserUseCase.UserUseCaseClient>();
            return await client.CreateUserAsync(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}