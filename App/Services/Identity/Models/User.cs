using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Identity.Models
{
    public class User : BaseEntity
    {
        public string Name {get;set;} = "";
    }
}