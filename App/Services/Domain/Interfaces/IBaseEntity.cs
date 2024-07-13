using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseEntity
    {
        Guid Id {get;set;}
        DateTime Inclusion {get;set;}
        DateTime Changed {get;set;}
        bool Excluded {get;set;}

    }
}