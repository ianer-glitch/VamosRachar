using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Identity.Models;
using Identity.Repositories.UserRepo;
using ProtoServer;
using ProtoServer.ProtoFiles;
using RabbitMQ.Client;
using System.Text;
using ServiceBusServer;


namespace Identity.UseCases.UserUseCase
{
    public class UserUseCase : ProtoServer.ProtoFiles.UserUseCase.UserUseCaseBase
    {
        private readonly IUserRepositroy _userRepositroy;
        private readonly IConfiguration _configuration;
        
        public UserUseCase(IUserRepositroy userRepositroy , IConfiguration conf)
        {
            _userRepositroy = userRepositroy;
            _configuration = conf;
        }
        public override async Task<PCreateUser> CreateUser(PCreateUser request, ServerCallContext context)
        {
            try
            {
                ServiceBusConections.SendObjectOnLogQueue(_configuration,"Creating User");
                // if (request is null)
                //     throw new ArgumentException("CreateUser payload is null!");
                //
                // if(string.IsNullOrEmpty(request.Email))
                //     throw new ArgumentException("User Has no email!");
                //
                // if(string.IsNullOrEmpty(request.Name))
                //     throw new ArgumentException("User Has no name!");
                //
                // if(!request.Password.Equals(request.PasswordConfirmation))
                //     throw new ArgumentException("Passwords not match!");
                //
                //
                // var response = await _userRepositroy.InsertUser(request);
                //
                return new PCreateUser
                {
                    Email = request.Email,
                    Name = request.Name,
                };

            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
            
           
        }
    }
}