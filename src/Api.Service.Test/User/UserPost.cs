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
  public class UserPost : UserTest
  {
    public IUserService _service;
    private Mock<IUserService> _serviceMock;

    [Fact(DisplayName = "É possível executar o método Post.")]
    public async Task Test_Execute_Method_Post()
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
    }
  }
}