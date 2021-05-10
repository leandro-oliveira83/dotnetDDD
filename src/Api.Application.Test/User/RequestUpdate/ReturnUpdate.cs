using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.RequestUpdate
{
  public class ReturnUpdate
  {
    private UsersController _controller;

    [Fact(DisplayName = "É possível realizar update.")]
    public async Task Test_Execute_Controller_Update()
    {
      var name = Faker.Name.FullName();
      var email = Faker.Internet.Email();
      var phone = "333-5555";

      // Mock Controller with IUserService
      var serviceMock = new Mock<IUserService>();
      serviceMock.Setup(m => m.Put(It.IsAny<UserDtoUpdate>())).ReturnsAsync(
        new UserDtoUpdateResult
        {
          Id = Guid.NewGuid(),
          Name = name,
          Email = email,
          Phone = phone,
          Sex = "M",
          UpdateAt = DateTime.UtcNow
        });
      _controller = new UsersController(serviceMock.Object);

      // Mock Url from controller
      Mock<IUrlHelper> url = new Mock<IUrlHelper>();
      url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<string>())).Returns("https://localhost:5000");
      _controller.Url = url.Object;

      var UserDtoUpdate = new UserDtoUpdate
      {
        Name = name,
        Email = email,
        Phone = phone,
        Sex = "M",
      };

      var result = await _controller.Put(UserDtoUpdate);
      Assert.True(result is OkObjectResult);

      var resultValue = ((OkObjectResult)result).Value as UserDtoUpdateResult;
      Assert.NotNull(resultValue);
      Assert.Equal(UserDtoUpdate.Name, resultValue.Name);
      Assert.Equal(UserDtoUpdate.Email, resultValue.Email);
    }
  }
}