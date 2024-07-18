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
            "User ID=root;Password=example;Host=db-identity;Port=5432;Database=myDataBase;Pooling=true;");
    }
}