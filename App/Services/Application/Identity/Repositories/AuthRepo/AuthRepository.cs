using Identity.Data;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProtoServer.ProtoFiles;

namespace Identity.Repositories.AuthRepo;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<User> _userManager;
    private readonly IdentityContext _identityContext;
    public AuthRepository(UserManager<User> userManager,IdentityContext identityContext)
    {
        _userManager = userManager;
        _identityContext = identityContext;
    }
    public async Task<PLoginRequest> UserLogin(PLoginRequest request)
    {
        try
        {
            User userToLogin =await  _identityContext.Users.FirstOrDefaultAsync(f => f.UserName == request.Email);
            
            
            var passwordValidation  =
                _userManager.PasswordHasher.VerifyHashedPassword(userToLogin, request.Password,
                    userToLogin.PasswordHash);

            if (passwordValidation == PasswordVerificationResult.Failed)
                throw new Exception("User or Password are invalid!");
            
            //return json


            return await Task.FromResult(request);


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}