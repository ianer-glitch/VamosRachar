using Domain.Interfaces;

namespace Identity.Models;

public class IdentityBaseEntity : IEntity
{
   
        public Guid Id {get;set;}
        public DateTime Inclusion {get;set;}
        public DateTime Changed {get;set;}
        public bool Excluded {get;set;}
}