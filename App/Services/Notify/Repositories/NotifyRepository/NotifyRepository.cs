using System.Linq.Expressions;
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
        var mongoClient = new MongoClient("mongodb://user:pass@notify-db:27017");
        var dbContextOptions =
            new DbContextOptionsBuilder<NotifyContext>().UseMongoDB(mongoClient, "notifify-db");
        _notify = new NotifyContext(dbContextOptions.Options);
    }
    
    public async  Task<T1> GetAllPagingAsync<T1>(int quanity, int currenctPage)
    {
        throw new NotImplementedException();
    }

    public async  Task<T> InsertAsync(T entity,Expression<Func<T,bool>> expre)
    {
        try
        {
            var existingModel = await _notify.Set<T>().FirstOrDefaultAsync(expre);
            if (existingModel is not null)
                throw new ArgumentException($"Can't create entity {nameof(entity)}, it already exist in database");
            
            await _notify.Set<T>().AddAsync(entity);
            await _notify.SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async  Task<T> UpdateAsync(T entity,Expression<Func<T,bool>> expre)
    {
        try
        {
            var existingModel = await _notify.Set<T>().FirstOrDefaultAsync(expre);
            if (existingModel is null)
                throw new ArgumentException($"Can't update entity {nameof(entity)}, it not exist in database");
            
            _notify.Set<T>().Update(entity);
            await _notify.SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async  Task<T> SetExcludedAsync(Expression<Func<T,bool>> expre) 
    {
        try
        {
            var model = await _notify.Set<T>().FirstOrDefaultAsync(expre);
            if (model is null)
                throw new ArgumentNullException(nameof(model));
            model.Excluded = true;

            if (await _notify.SaveChangesAsync() == 0)
                throw new Exception($"Could not set{nameof(model)} to excluded");
            
            return model;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }

    public async  Task<T> SetActiveAsync(Expression<Func<T,bool>> expre)
    {
        try
        {
            var model = await _notify.Set<T>().FirstOrDefaultAsync(expre);
            if (model is null)
                throw new ArgumentNullException(nameof(model));
            model.Excluded = false;

            if (await _notify.SaveChangesAsync() == 0)
                throw new Exception($"Could not set{nameof(model)} to active");
            
            return model;
            

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}