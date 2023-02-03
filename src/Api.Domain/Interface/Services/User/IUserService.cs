using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Dtos.User;

namespace Api.Domain.Interface.Services.User
{
    public interface IUserService
    {
        Task<UserDto?> Get(Guid id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDtoCreateResult> Post(UserDtoCreate user);
        Task<UserDtoUpdateResult?> Put(UserDtoUpdate user);
        Task<bool> Delete(Guid id);
    }
}
