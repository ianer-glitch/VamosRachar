using ProtoServer.ConnectionHelpers;
using ProtoServer.ProtoFiles;

namespace Gateway.Services.User;

public class UserService : IUserService
{
    public async Task<PCreateUser> CreateUser( PCreateUser request)
    {
        try
        {
            var client = MicroserviceConnection.GetIdentityClient<UserUseCase.UserUseCaseClient>();
            return await client.CreateUserAsync(request);
        }
        catch (Exception e)
        {
            
            throw new Exception(e.Message);
        }
    }
}