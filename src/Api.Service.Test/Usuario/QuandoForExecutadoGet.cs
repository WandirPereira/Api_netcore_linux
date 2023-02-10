using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interface.Services.User;
using Moq;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecutadoGet : UsuarioTestes
    {
        private IUserService? _service;
        private Mock<IUserService>? _serviceMock;

        [Fact(DisplayName = "É possível executar o método GET")]
        public async Task EhPossivelExecutarMetodoGet()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Get(IdUsuario)).ReturnsAsync(userDto);
            //ReturnsAsync(userDto); --> retorna o userDto instanciado e populado em UsuarioTestes, quando informado o Get para o IdUsuario criado no UsuarioTestes
            //Não está indo ao BD, apenas usando os dados de UsuarioTestes. O repositório já foi testado em Api.Data.test
            _service = _serviceMock.Object;
            var result = await _service.Get(IdUsuario);
            Assert.NotNull(result);
            Assert.True(result.Id == IdUsuario);
            Assert.Equal(NomeUsuario, result.Name);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UserDto?)null));
            //(UserDto?)null --> faz casting de null para o tipo USerDto Nullable
            //Task.FromResult((UserDto?)null)  --> cria uma Task que retorna um UserDto nullo no caso de sucesso 
            //It.IsAny<Guid>() --> aceita qualquer valor de Guid. 
            //It.IsAny<Guid>() --> retorna o valor default para o tipo Guid {00000000-0000-0000-0000-000000000000}
            _service = _serviceMock.Object;

            var _record = await _service.Get(IdUsuario);
            Assert.Null(_record);
        }
    }
}
