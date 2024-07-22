using ProtoServer.ProtoFiles;

namespace Gateway.Services.Auth;

public interface IAuthService
{
    Task<PAuthToken> Login(PLoginRequest request);
}

