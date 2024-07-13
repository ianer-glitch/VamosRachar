using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;
using Identity.Repositories.UserRepo;

namespace Identity.UseCases.UserCase
{
    public class UseCaseUser : IUseCaseUser
    {
        private readonly IUserRepositroy _userRepo;
        public UseCaseUser(IUserRepositroy userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<User> CreateUser(User newUser)
        {
            try
            {
                if(newUser.Name is null)
                    throw new ArgumentNullException("User Has no name");
                
                return await _userRepo.InsertUser(newUser);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
                
        }
    }
}