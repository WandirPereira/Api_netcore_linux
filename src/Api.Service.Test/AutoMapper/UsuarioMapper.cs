using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Models;

namespace Api.Service.Test.AutoMapper
{
    public class UsuarioMapper : BaseTesteService
    {
        [Fact(DisplayName = "É possivel mapear os modelos")]
        public void EhPossivelMapearOsModelos()
        {

            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            //Model ==> Entidade
            var dtoToEntity = Mapper.Map<UserEntity>(model);
            Assert.Equal(dtoToEntity.Id, model.Id);
            Assert.Equal(dtoToEntity.Name, model.Name);
            Assert.Equal(dtoToEntity.Email, model.Email);
            Assert.Equal(dtoToEntity.CreateAt, model.CreateAt);
            Assert.Equal(dtoToEntity.UpdateAt, model.UpdateAt);

        }
    }
}
