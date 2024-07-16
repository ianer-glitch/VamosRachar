using Identity.Models;
using Identity.Repositories.IdentityRepo;

namespace Identity.Repositories.UserRepo;

public class UserRepository :IUserRepositroy
{
    private readonly IIdentityRepository<User> _identityRepo;
    
    public UserRepository(IIdentityRepository<User> identityRepo)
    {
        _identityRepo = identityRepo;
    }

    public async Task<User> InserOrUpdatetUser(User newUser)
    {
        try
        {
            if(newUser is null)
                throw new ArgumentException("No user to create");
                    
            return await Task.FromResult(newUser);
            //return  await _identityRepo.InsertOrUpdate( newUser);
        }
        catch(Exception ex){
            throw new Exception(ex.Message);
        }
            
    }
    
}