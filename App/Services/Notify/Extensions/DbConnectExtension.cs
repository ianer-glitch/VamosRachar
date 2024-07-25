using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Notify.Data;

namespace Notify.Extensions;

public static  class DbConnectExtension
{
    public static NotifyContext GetDbContextInstance(IConfiguration conf)
    {
        
        string connectionString = conf
            .GetSection("ConnectionStrings")
            .GetSection("NotifyDb")?
            .Value ?? throw new ArgumentNullException("there's no connection string for NotifyDb");
        
        var mongoClient = new MongoClient(connectionString);
        
        string databaseName = conf
            .GetSection("DatabaseName")
            .Value ?? throw new ArgumentNullException("there's no database name for NotifyDb");
        
        var dbContextOptions =
            new DbContextOptionsBuilder<NotifyContext>().UseMongoDB(mongoClient, databaseName);
        return new NotifyContext(dbContextOptions.Options);
    }
}