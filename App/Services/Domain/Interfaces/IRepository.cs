using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Domain.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetAllPagingAsync<T>(int quanity, int currenctPage );

        Task<T> InsertOrUpdateAsync(T entity);

        Task<T> SetExcludedAsync(T entity);

        Task<T> SetActiveAsync(T entity);
    }
}