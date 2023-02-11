using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interface.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Usuario.QuandoRequisitarGet
{
    public class Retorno_Get
    {
        private UsersController? _controller;
        [Fact(DisplayName = "É possível realizar o GET.")]
        public async Task EhPossivelInvocarControllerGet()
        {
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();


            serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    CreateAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(serviceMock.Object); //UsersController recebe um IUserService

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            UserDto? resultValue = ((OkObjectResult)result).Value as UserDto;
            Assert.NotNull(resultValue);
            Assert.Equal(nome, resultValue.Name);
            Assert.Equal(email, resultValue.Email);
        }
    }
}
