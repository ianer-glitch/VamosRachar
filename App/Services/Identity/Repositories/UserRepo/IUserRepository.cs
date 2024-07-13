using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Identity.Models;

namespace Identity.Repositories.UserRepo
{
    public interface IUserRepositroy
    {
        public Task<User> InsertUser(User newUser);
    }
}