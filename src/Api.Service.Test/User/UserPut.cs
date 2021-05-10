using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Users;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
  public class UserPut : UserTest
  {
    public IUserService _service;
    private Mock<IUserService> _serviceMock;

    [Fact(DisplayName = "É possível executar o método Put.")]
    public async Task Test_Execute_Method_Put()
    {
      // Mock method Post from Service layer
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Post(userDtoCreate)).ReturnsAsync(userDtoCreateResult);
      _service = _serviceMock.Object;

      var result = await _service.Post(userDtoCreate);
      Assert.NotNull(result);
      Assert.Equal(Name, result.Name);
      Assert.Equal(Email, result.Email);
      Assert.Equal(Phone, result.Phone);
      Assert.Equal(Sex, result.Sex);

      // Mock method Put from Service layer
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Put(userDtoUpdate)).ReturnsAsync(userDtoUpdateResult);
      _service = _serviceMock.Object;

      var resultUpdate = await _service.Put(userDtoUpdate);
      Assert.NotNull(result);
      Assert.Equal(NameUpdate, resultUpdate.Name);
      Assert.Equal(EmailUpdate, resultUpdate.Email);
      Assert.Equal(PhoneUpdate, resultUpdate.Phone);
      Assert.Equal(SexUpdate, resultUpdate.Sex);
    }
  }
}