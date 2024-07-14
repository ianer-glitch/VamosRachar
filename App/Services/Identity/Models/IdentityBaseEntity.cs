using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;


namespace Identity.Models
{
    public class IdentityBaseEntity  : IEntity
    {
        public Guid Id {get;set;}
        public DateTime Inclusion {get;set;}
        public DateTime Changed {get;set;}
        public bool Excluded {get;set;}
        
    
    }
}