using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;


namespace Domain.Interfaces
{
    public interface IBaseUseCase<T> 
    {
        Task<T> GetAllPaging<T>(int Quanity, int CurrenctPage ) where T: BaseEntity;

        Task<T> InsertOrUpdate(T Entity);

        Task<T> SetExcluded(T Entity);

        Task<T> SetActive(T Entity);
    }
}