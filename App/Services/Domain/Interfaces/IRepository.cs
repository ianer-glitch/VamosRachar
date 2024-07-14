using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Domain.Interfaces
{
    public interface IRepository<T> 
    {
        Task<T> GetAllPaging<T>(int Quanity, int CurrenctPage );

        Task<T> InsertOrUpdate(T Entity);

        Task<T> SetExcluded(T Entity);

        Task<T> SetActive(T Entity);
    }
}