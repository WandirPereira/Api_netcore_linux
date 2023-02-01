using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interface.Services.User;
using Api.Domain.Repository;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;

        public LoginService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<object?> FindByLogin(LoginDto login)
        {
            // if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            //     return await _repository.FindByLogin(user.Email);
            // else
            //     return null;

            return await _repository.FindByLogin(login.Email!);
        }
    }
}
