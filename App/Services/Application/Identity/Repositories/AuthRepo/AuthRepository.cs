using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Identity.Data;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProtoServer.ProtoFiles;

namespace Identity.Repositories.AuthRepo;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<User> _userManager;
    private readonly IdentityContext _identityContext;
    
    public AuthRepository(
            UserManager<User> userManager,
            IdentityContext identityContext 
            )
    {
        _userManager = userManager;
        _identityContext = identityContext;
        
    }
    public async Task<bool> IsUserPasswordValid(PLoginRequest request)
    {
        try
        {
            User userToLogin =await  _identityContext.Users.FirstOrDefaultAsync(f => f.UserName == request.Email);
            
            if(userToLogin is null)
                throw new ArgumentNullException("User not found!");
            
            return  await _userManager.CheckPasswordAsync(userToLogin, request.Password);
            


         


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

   
}