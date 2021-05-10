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
      _controller.ModelState.AddModelError("Id", "Formato Inválido");

      var result = await _controller.Get(Guid.NewGuid());
      Assert.True(result is BadRequestObjectResult);
    }
  }
}