using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.RequestCreate
{
  public class ReturnCreate
  {
    private UsersController _controller;

    [Fact(DisplayName = "É possível realizar create.")]
    public async Task Test_Execute_Controller_Create()
    {
      var name = Faker.Name.FullName();
      var email = Faker.Internet.Email();
      var phone = "333-5555";

      // Mock Controller with IUserService
      var serviceMock = new Mock<IUserService>();
      serviceMock.Setup(m => m.Post(It.IsAny<UserDtoCreate>())).ReturnsAsync(
        new UserDtoCreateResult
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

      var UserDtoCreate = new UserDtoCreate
      {
        Name = name,
        Email = email,
        Phone = phone,
        Sex = "M",
      };

      var result = await _controller.Post(UserDtoCreate);
      Assert.True(result is CreatedResult);

      var resultValue = ((CreatedResult)result).Value as UserDtoCreateResult;
      Assert.NotNull(resultValue);
      Assert.Equal(UserDtoCreate.Name, resultValue.Name);
      Assert.Equal(UserDtoCreate.Email, resultValue.Email);
    }
  }
}