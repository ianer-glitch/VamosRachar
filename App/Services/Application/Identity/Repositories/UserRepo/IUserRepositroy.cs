using Identity.Models;
using ProtoServer.ProtoFiles;

namespace Identity.Repositories.UserRepo;

public interface IUserRepositroy
{
    public Task<User> InsertUser(PCreateUser newUser);
}