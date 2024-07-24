using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Notify.Data;

namespace Notify.Repositories.NotifyRepository;

public class NotifyRepository<T> : INotifyRepository<T> where T : class,IEntity
{
    private readonly NotifyContext _notify;
    public NotifyRepository()
    {
        var mongoClient = new MongoClient("<Your MongoDB Connection URI>");
        var dbContextOptions =
            new DbContextOptionsBuilder<NotifyContext>().UseMongoDB(mongoClient, "<Database Name");
        
        _notify = new NotifyContext(dbContextOptions.Options);
    }
    
    public async  Task<T1> GetAllPagingAsync<T1>(int quanity, int currenctPage)
    {
        throw new NotImplementedException();
    }

    public async  Task<T> InsertOrUpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async  Task<T> SetExcludedAsync(T entity) 
    {
        if (entity is null)
            throw new ArgumentNullException("entity is null!");

        var n = await _notify.Set<T>().FirstOrDefaultAsync();
        n.Excluded = true;

        return n;

    }

    public async  Task<T> SetActiveAsync(T entity)
    {
        throw new NotImplementedException();
    }
}