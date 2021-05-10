using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.RequestDelete
{
  public class ReturnBadRequest
  {
    private UsersController _controller;

    [Fact(DisplayName = "É possível teste bad request.")]
    public async Task Test_Execute_Controller_BadRequest()
    {
      // Mock Controller with IUserService
      var serviceMock = new Mock<IUserService>();
      serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
      _controller = new UsersController(serviceMock.Object);
      _controller.ModelState.AddModelError("Id", "Formato inválido");

      // Mock Url from controller
      Mock<IUrlHelper> url = new Mock<IUrlHelper>();
      url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<string>())).Returns("https://localhost:5000");
      _controller.Url = url.Object;

      var result = await _controller.Delete(default(Guid));
      Assert.True(result is BadRequestObjectResult);
      Assert.False(_controller.ModelState.IsValid);
    }
  }
}