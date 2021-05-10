using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.Users;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
  public class UserGetAll : UserTest
  {
    public IUserService _service;
    private Mock<IUserService> _serviceMock;

    [Fact(DisplayName = "É possível executar o método GetALL.")]
    public async Task Test_Execute_Method_GetAll()
    {
      // Mock method GetAll from Service layer
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(listUserDto);
      _service = _serviceMock.Object;

      var result = await _service.GetAll();
      Assert.NotNull(result);
      Assert.True(result.Count() == 10);

      // Mock method Get from Service layer, test empty list
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(new List<UserDto>().AsEnumerable);
      _service = _serviceMock.Object;

      var _resultEmpty = await _service.GetAll();
      Assert.Empty(_resultEmpty);
      Assert.True(_resultEmpty.Count() == 0);
    }
  }
}