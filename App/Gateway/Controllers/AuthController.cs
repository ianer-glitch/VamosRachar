using Gateway.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using ProtoServer.ProtoFiles;

namespace Gateway.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }
    [HttpPost]
    public async Task<ActionResult<PAuthToken>> Login(PLoginRequest resquest)
    {
        try
        {
            return await _auth.Login(resquest);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}