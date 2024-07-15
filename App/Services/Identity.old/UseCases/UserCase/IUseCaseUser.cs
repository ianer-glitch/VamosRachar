using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;

namespace Identity.UseCases.UserCase
{
    public interface IUseCaseUser
    {
        Task<User> CreateUser(User newUser);
    }
}