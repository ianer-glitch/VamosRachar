using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;


namespace Domain.Interfaces
{
    public interface IBaseUseCase<T> where T: BaseEntity
    {
        T GetAllPaging(int Quanity, int CurrenctPage );

        T InsertOrUpdate(T Entity);

        T SetExcluded(T Entity);

        T SetActive(T Entity);
    }
}