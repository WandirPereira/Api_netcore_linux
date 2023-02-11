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
    public class Retorno_BadRequest
    {
        private UsersController? _controller;
        [Fact(DisplayName = "É possível realizar o DELETE.")]
        public async Task EhPossivelInvocarControllerDelete()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);

            _controller = new UsersController(serviceMock.Object); //UsersController recebe um IUserService
            _controller.ModelState.AddModelError("Id", "Formato Inválido!");  //Adiciona um erro no ModelState

            var result = await _controller.Delete(Guid.NewGuid());
            //var result = await _controller.Delete(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }
    }
}
