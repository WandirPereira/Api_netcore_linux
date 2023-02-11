using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interface.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Usuario.QuandoRequisitarDelete
{
    public class Retorno_Delete
    {
        private UsersController? _controller;
        [Fact(DisplayName = "É possível realizar o DELETE.")]
        public async Task EhPossivelInvocarControllerDelete()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new UsersController(serviceMock.Object); //UsersController recebe um IUserService

            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            // var resultValue = ((OkObjectResult)result).Value;
            // Assert.NotNull(resultValue);
            // Assert.True((Boolean)resultValue);
            //ou
            var resultValue = ((OkObjectResult)result).Value as Boolean?;
            Assert.NotNull(resultValue);
            Assert.True(resultValue);
        }
    }
}
