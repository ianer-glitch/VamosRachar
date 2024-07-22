using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Grpc.Core;
using Identity.Repositories.AuthRepo;
using Microsoft.IdentityModel.Tokens;
using ProtoServer.ProtoFiles;

namespace Identity.UseCases.AuthUseCase;

public class AuthUseCase : ProtoServer.ProtoFiles.AuthUseCase.AuthUseCaseBase
{
    private readonly IConfiguration _conf;
    private readonly IAuthRepository _auth;
    public AuthUseCase(IConfiguration conf,IAuthRepository auth)
    {
        _conf = conf;
        _auth = auth;
    }
    public override async Task<PAuthToken> UserLogin(PLoginRequest request, ServerCallContext context)
    {
        try
        {

            bool isValid = await _auth.IsUserPasswordValid(request);

            if (!isValid)
            {
                throw new ArgumentException("User password is invalid!");
            }
            
            PAuthToken response = new()
            {
                Token = GenerateToken()
            };

            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private string GenerateToken()
    { //TODO: add clains to token
        var jwtConfiguration = _conf.GetSection("JwtConfiguration");
        
        var issuer = jwtConfiguration.GetSection("Issuer").Value ?? string.Empty; 
        var audience = jwtConfiguration.GetSection("Audience").Value ?? string.Empty;
        
        DateTime expires = DateTime.Now.AddMinutes(
            int.Parse(
                jwtConfiguration.GetSection("ExpirationTimeInMinutes").Value ?? string.Empty
            )
        );
        
        var securityKey = 
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    jwtConfiguration.GetSection("SecurityKey").Value ?? string.Empty
                )
            );
        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: expires,
            signingCredentials: credentials
        );
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }
}