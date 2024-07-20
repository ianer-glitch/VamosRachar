using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data;

public class IdentityContext : IdentityDbContext<User>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base (options)
    {
        
    }
  
    protected  override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(
            "User ID=postgres;Password=admin;Host=db-identity;Port=5432;Database=postgres;Pooling=true;");
        
        //"User ID=root;Password=admin;Host=localhost;Port=5011;Database=myDataBase;Pooling=true;"); >> connection string from host
        //User ID=postgres;Password=admin;Host=localhost;Port=5011;Database=postgres;Pooling=true;
        
    }
}