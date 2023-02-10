using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Interface.Services.User;
using Moq;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecutadoDelete : UsuarioTestes
    {
        private IUserService? _service;
        private Mock<IUserService>? _serviceMock;

        [Fact(DisplayName = "É possível executar o método DELETE")]
        public async Task EhPossivelExecutarMetodoDelete()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
            _service = _serviceMock.Object;
            var resultDelete = await _service.Delete(IdUsuario);
            Assert.True(resultDelete);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMock.Object;
            resultDelete = await _service.Delete(IdUsuario);
            //ou resultDelete = await _service.Delete(Guid.NewGuid());
            Assert.False(resultDelete);
        }
    }
}
