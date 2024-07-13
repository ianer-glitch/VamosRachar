using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;
using Domain.Interfaces;
using Domain.Models;

namespace Identity.UseCases
{
    public class IdentityBaseUseCase<T> : IBaseUseCase<T> where T : BaseEntity
    {
        public T GetAllPaging(int Quanity, int CurrenctPage)
        {
            throw new NotImplementedException();
        }

        public T InsertOrUpdate(T Entity)
        {
            throw new NotImplementedException();
        }

        public T SetActive(T Entity)
        {
            throw new NotImplementedException();
        }

        public T SetExcluded(T Entity)
        {
            throw new NotImplementedException();
        }
    }
}