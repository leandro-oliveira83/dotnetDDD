using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.RequestGet
{
  public class ReturnGet
  {
    private UsersController _controller;

    [Fact(DisplayName = "É possível realizar Get.")]
    public async Task Test_Execute_Controller_Get()
    {
      var name = Faker.Name.FullName();
      var email = Faker.Internet.Email();
      var phone = "333-5555";

      // Mock Controller with IUserService
      var serviceMock = new Mock<IUserService>();
      serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
        new UserDto
        {
          Id = Guid.NewGuid(),
          Name = name,
          Email = email,
          Phone = phone,
          Sex = "M",
          CreateAt = DateTime.UtcNow
        });
      _controller = new UsersController(serviceMock.Object);

      // Mock Url from controller
      Mock<IUrlHelper> url = new Mock<IUrlHelper>();
      url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<string>())).Returns("https://localhost:5000");
      _controller.Url = url.Object;

      var result = await _controller.Get(Guid.NewGuid());
      Assert.True(result is OkObjectResult);

      var resultValue = ((OkObjectResult)result).Value as UserDto;
      Assert.NotNull(resultValue);
      Assert.Equal(name, resultValue.Name);
      Assert.Equal(email, resultValue.Email);
    }
  }
}