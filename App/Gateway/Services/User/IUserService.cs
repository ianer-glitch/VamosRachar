using ProtoServer.ProtoFiles;

namespace Gateway.Services.User;

public interface IUserService
{
    Task<PCreateUser> CreateUser(PCreateUser request);
}