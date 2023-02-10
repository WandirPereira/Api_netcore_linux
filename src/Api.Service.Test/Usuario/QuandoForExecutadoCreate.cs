using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interface.Services.User;
using Moq;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecutadoCreate : UsuarioTestes
    {
        private IUserService? _service;
        private Mock<IUserService>? _serviceMock;

        [Fact(DisplayName = "É possível executar o método CREATE")]
        public async Task EhPossivelExecutarMetodoCreate()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Post(userDtoCreate)).ReturnsAsync(userDtoCreateResult);
            _service = _serviceMock.Object;
            var result = await _service.Post(userDtoCreate);
            Assert.NotNull(result);
            Assert.Equal(NomeUsuario, result.Name);
            Assert.Equal(EmailUsuario, result.Email);
        }
    }
}
