using ProtoServer.ProtoFiles;

namespace Gateway.Services.Auth;

public interface IAuthService
{
    Task<PLoginRequest> Login(PLoginRequest request);
}

