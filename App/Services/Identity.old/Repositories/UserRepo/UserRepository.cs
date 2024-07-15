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
        private readonly IIdentityRepository<User> _identiyRepo;
        public UserRepository(IIdentityRepository<User> baseCase)
        {
            _identiyRepo = baseCase;
        }

        public async Task<User> InsertUser(User newUser)
        {
            try
            {
                if(newUser is null)
                    throw new ArgumentException("No user to create");
                    
                return await Task.FromResult(newUser);
                //return  await _identiyRepo.InsertOrUpdate( newUser);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            
        }
    }
}