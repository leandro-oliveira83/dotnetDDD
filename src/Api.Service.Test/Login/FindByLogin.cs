using System;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.Users;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
  public class FindByLogin
  {
    public ILoginService _service;
    private Mock<ILoginService> _serviceMock;

    [Fact(DisplayName = "É possível executar o método FindByLogin.")]
    public async Task Test_Execute_Method_FindByLogin()
    {
      var email = Faker.Internet.Email();
      var objectReturn = new
      {
        authenticated = true,
        create = DateTime.UtcNow,
        expiration = DateTime.UtcNow.AddHours(2),
        accessToken = Guid.NewGuid(),
        email = email,
        name = Faker.Name.FullName(),
        message = "Usuário logado com sucesso."
      };

      var loginDto = new LoginDto
      {
        Email = email
      };

      var loginDto2 = new LoginDto
      {
        Email = "leandro@gmai.com"
      };

      // Mock method Post from Service layer
      _serviceMock = new Mock<ILoginService>();
      _serviceMock.Setup(m => m.FindByLogin(loginDto)).ReturnsAsync(objectReturn);
      _service = _serviceMock.Object;

      var result = await _service.FindByLogin(loginDto);
      Assert.NotNull(result);
    }
  }
}