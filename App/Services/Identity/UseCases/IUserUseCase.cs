using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Identity.Models;
namespace Identity.UseCases
{
    public interface IUserUseCase
    {
        private readonly IdentityBaseUseCase<User> _baseCase;
        public IUserUseCase(Parameters)
        {
            
        }
        public Task<User> InsertUser();
    }
}