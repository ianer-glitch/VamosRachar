using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;

namespace Identity.UseCases
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IIdentityBaseUseCase<User> _baseCase;
        public UserUseCase(IIdentityBaseUseCase<User> baseCase)
        {
            _baseCase = baseCase;
        }

        public async Task<User> InsertUser()
        {
            return  await _baseCase.InsertOrUpdate( new User());
            
        }
    }
}