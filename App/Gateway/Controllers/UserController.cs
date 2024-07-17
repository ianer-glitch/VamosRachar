using Gateway.Services.User;
using Microsoft.AspNetCore.Mvc;
using ProtoServer.ConnectionHelpers;
using ProtoServer.ProtoFiles;

namespace Gateway.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    
    [HttpPost]
    [Route("CreateUser")]
    public async Task<ActionResult<PCreateUser>> CreateUser([FromBody] PCreateUser request)
    {
        try
        {
            var res = await _userService.CreateUser(request);

            if (res is not null)
                    return Ok(res);

            return BadRequest();
            
      
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
            throw;
        }
    }
    
}