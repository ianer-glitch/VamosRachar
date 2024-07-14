using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Identity.Data
{
    public class IdentityContext : IdentityDbContext
    {
        public DbSet<User> Users {get;set;}
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            
        }
        
    }
}
