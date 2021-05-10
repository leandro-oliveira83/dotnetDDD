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
  public class ReturnBadRequest
  {
    private UsersController _controller;

    [Fact(DisplayName = "É possível teste bad request.")]
    public async Task Test_Execute_Controller_BadRequest()
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
      _controller.ModelState.AddModelError("Name", "É uma campo Obrigatório");

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
      Assert.True(result is BadRequestObjectResult);
    }
  }
}