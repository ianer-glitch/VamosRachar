using Microsoft.AspNetCore.Mvc;
using ProtoServer.ProtoFiles;

namespace Gateway.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Login(PLoginRequest resquest)
    {
        try
        {
            return await Task.FromResult(Ok());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}