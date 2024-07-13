using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;
using Identity.Repositories.IdentityRepo;

namespace Identity.Repositories.UserRepo
{
    public class UserRepository : IUserRepositroy
    {
        private readonly IIdentityRepository<User> _baseCase;
        public UserRepository(IIdentityRepository<User> baseCase)
        {
            _baseCase = baseCase;
        }

        public async Task<User> InsertUser()
        {
            return  await _baseCase.InsertOrUpdate( new User());
            
        }
    }
}