using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data;

public class IdentityContext : IdentityDbContext<User>
{
    private readonly IConfiguration _conf;
    public IdentityContext(DbContextOptions<IdentityContext> options,IConfiguration conf) : base (options)
    {
        _conf = conf;
    }
  
    protected  override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string dbConnectionString =
            _conf.GetSection("ConnectionStrings").GetSection("IdentityDb").Value ?? string.Empty;
        
        if(string.IsNullOrEmpty(dbConnectionString))
            throw new ArgumentNullException("IdentityDb connection string could not be found");

        options.UseNpgsql(dbConnectionString);
    }
}