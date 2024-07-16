using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Identity.Models;
using Identity.Repositories.UserRepo;
using ProtoServer;
using ProtoServer.ProtoFiles;

namespace Identity.UseCases.UserUseCase
{
    public class UserUseCase :ProtoServer.ProtoFiles.UserUseCase.UserUseCaseBase
    {
        private readonly IUserRepositroy _userRepositroy;

        public UserUseCase(IUserRepositroy userRepositroy)
        {
            _userRepositroy = userRepositroy;
        }
        public override async Task<PCreateUser> CreateUser(PCreateUser request, ServerCallContext context)
        {
            try
            {
                if (request is null)
                    throw new ArgumentException("CreateUser payload is null!");

                if(string.IsNullOrEmpty(request.Email))
                    throw new ArgumentException("User Has no email!");
                
                if(string.IsNullOrEmpty(request.Name))
                    throw new ArgumentException("User Has no name!");

                User newUser = new()
                {
                    Id = Guid.NewGuid(),
                    Changed = DateTime.Now,
                    Inclusion = DateTime.Now,
                    Email = "email@email.com",
                    Name = "João",
                    Excluded = false,
                };
                
                var response = await _userRepositroy.InserOrUpdatetUser(newUser);

                return new PCreateUser
                {
                    Email = response.Email,
                    Name = response.Name,
                };

            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
            
           
        }


    }
}