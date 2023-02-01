using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;

namespace Api.Domain.Interface.Services.User
{
    public interface ILoginService
    {
        Task<object?> FindByLogin(LoginDto login);
    }
}
