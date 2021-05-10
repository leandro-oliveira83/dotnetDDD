using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Users;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
  public class UserDelete : UserTest
  {
    public IUserService _service;
    private Mock<IUserService> _serviceMock;

    [Fact(DisplayName = "É possível executar o método Delete.")]
    public async Task Test_Execute_Method_Delete()
    {
      // Mock method Delete from Service layer
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
      _service = _serviceMock.Object;

      var delete = await _service.Delete(Id);
      Assert.True(delete);

      // Mock method Delete from Service layer
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
      _service = _serviceMock.Object;

      delete = await _service.Delete(Id);
      Assert.False(delete);
    }
  }
}