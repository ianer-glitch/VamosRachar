using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;
using Domain.Interfaces;
using Domain.Models;

namespace Identity.UseCases
{
    public class IdentityBaseUseCase<T> : IBaseUseCase<T>
    {
        public Task<T1> GetAllPaging<T1>(int Quanity, int CurrenctPage) where T1 : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task<T> InsertOrUpdate(T Entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> SetActive(T Entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> SetExcluded(T Entity)
        {
            throw new NotImplementedException();
        }
    }
}