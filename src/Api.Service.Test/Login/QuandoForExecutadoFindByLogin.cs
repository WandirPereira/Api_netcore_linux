using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Interface.Services.User;
using Moq;

namespace Api.Service.Test.Login
{
    public class QuandoForExecutadoFindByLogin
    {
        private ILoginService? _service;
        private Mock<ILoginService>? _serviceMock;

        [Fact(DisplayName = "É possível executar o método FindByLogin")]
        public async Task EhPossivelExecutarMetodoFindByLogin()
        {
            var email = Faker.Internet.Email();
            var objetoRetorno = new
            {
                authenticated = true,
                create = DateTime.UtcNow,
                expiration = DateTime.UtcNow.AddHours(8),
                accessToken = Guid.NewGuid(), //só para testar usando Guid no lugar do Token
                userName = email,
                name = Faker.Name.FullName(),
                message = "Usuário Logado com sucesso!"
            };

            var loginDto = new LoginDto
            {
                Email = email
            };

            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(m => m.FindByLogin(loginDto)).ReturnsAsync(objetoRetorno);
            _service = _serviceMock.Object;

            var result = await _service.FindByLogin(loginDto);
            Assert.NotNull(result);

        }
    }
}
