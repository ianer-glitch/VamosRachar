using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using Notify.Models;

namespace Notify.Data;

public class NotifyContext : DbContext
{
    public DbSet<Notification> Notifications { get; init; }
    
    public NotifyContext(DbContextOptions options ) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Notification>().ToCollection("notifications");
    }
}