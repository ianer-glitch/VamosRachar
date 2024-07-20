using ProtoServer.ConnectionHelpers;
using ProtoServer.ProtoFiles;

namespace Gateway.Services.Auth;

public class AuthService : IAuthService
{
    public async Task<PLoginRequest> Login(PLoginRequest request)
    {
        var client = MicroserviceConnection.GetIdentityClient<AuthUseCase.AuthUseCaseClient>();
        var response = await client.UserLoginAsync(request);
        return response;
    }
}