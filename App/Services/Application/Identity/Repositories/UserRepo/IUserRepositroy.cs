using Identity.Models;

namespace Identity.Repositories.UserRepo;

public interface IUserRepositroy
{
    public Task<User> InserOrUpdatetUser(User newUser);
}