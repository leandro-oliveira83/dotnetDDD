using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.Users;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
  public class UserGet : UserTest
  {
    public IUserService _service;
    private Mock<IUserService> _serviceMock;

    [Fact(DisplayName = "É possível executar o método Get.")]
    public async Task Test_Execute_Method_Get()
    {
      // Mock method Get from Service layer
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Get(Id)).ReturnsAsync(userDto);
      _service = _serviceMock.Object;

      var result = await _service.Get(Id);
      Assert.NotNull(result);
      Assert.True(result.Id == Id);
      Assert.Equal(Name, result.Name);

      // Mock method Get from Service layer
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UserDto)null));
      _service = _serviceMock.Object;

      var _record = await _service.Get(Id);
      Assert.Null(_record);
    }
  }
}