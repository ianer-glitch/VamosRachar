using Identity.Data;
using Identity.Models;
using Identity.Repositories.IdentityRepo;
using Microsoft.AspNetCore.Identity;
using ProtoServer.ProtoFiles;

namespace Identity.Repositories.UserRepo;

public class UserRepository :IUserRepositroy
{
    private readonly IIdentityRepository<User> _identityRepo;
    private readonly IdentityContext _context;
    private readonly UserManager<User> _userManager;
    
    public UserRepository(IIdentityRepository<User> identityRepo,UserManager<User> userManager)
    {
        _identityRepo = identityRepo;
        _userManager = userManager;

    }

    private async Task<User> InserOrUpdatetUser(User newUser)
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

    public async Task<User> InsertUser(PCreateUser request)
    {
        try
        {
            if(request is null)
                throw new ArgumentException("No user to create");
            
            
            User newUser = new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.Email,
                Name = request.Name,
                UserName = request.Email,
                
            };
            
            var password = _userManager
                    .PasswordHasher
                    .HashPassword(newUser,request.PasswordConfirmation);
            
            newUser.PasswordHash = password;
            
            var response = await _userManager.CreateAsync(newUser);
            if(response.Errors.Any())
                response.Errors.ToList().ForEach(f=> throw new Exception(f.Description));

            return newUser;

            //_context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}