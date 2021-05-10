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
  public class ReturnUpdate
  {
    private UsersController _controller;

    [Fact(DisplayName = "É possível realizar delete.")]
    public async Task Test_Execute_Controller_Delete()
    {
      // Mock Controller with IUserService
      var serviceMock = new Mock<IUserService>();
      serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
      _controller = new UsersController(serviceMock.Object);

      // Mock Url from controller
      Mock<IUrlHelper> url = new Mock<IUrlHelper>();
      url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<string>())).Returns("https://localhost:5000");
      _controller.Url = url.Object;

      var result = await _controller.Delete(Guid.NewGuid());
      Assert.True(result is OkObjectResult);

      var resultValue = ((OkObjectResult)result).Value;
      Assert.NotNull(resultValue);
      Assert.True((Boolean)resultValue);
    }
  }
}