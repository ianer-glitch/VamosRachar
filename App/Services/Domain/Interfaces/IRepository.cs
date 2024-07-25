using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;



namespace Domain.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetAllPagingAsync<T>(int quanity, int currenctPage );

        Task<T> InsertAsync(T entity,Expression<Func<T,bool>> expre);
        
        Task<T> UpdateAsync(T entity,Expression<Func<T,bool>> expre);

        Task<T> SetExcludedAsync(Expression<Func<T, bool>> expre);

        Task<T> SetActiveAsync(Expression<Func<T, bool>> expre);
    }
}