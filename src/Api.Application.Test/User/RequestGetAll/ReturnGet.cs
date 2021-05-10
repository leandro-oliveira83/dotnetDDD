using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.RequestGetAll
{
  public class ReturnGet
  {
    private UsersController _controller;

    [Fact(DisplayName = "É possível realizar GetAll.")]
    public async Task Test_Execute_Controller_GetAll()
    {
      // Mock Controller with IUserService
      var serviceMock = new Mock<IUserService>();
      serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
        new List<UserDto>{
          new UserDto
          {
            Id = Guid.NewGuid(),
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email(),
            Phone = "3333-4444",
            Sex = "M",
            CreateAt = DateTime.UtcNow
          },
          new UserDto
          {
            Id = Guid.NewGuid(),
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email(),
            Phone = "3333-5555",
            Sex = "M",
            CreateAt = DateTime.UtcNow
          }
        });
      _controller = new UsersController(serviceMock.Object);

      var result = await _controller.GetAll();
      Assert.True(result is OkObjectResult);

      var resultValue = ((OkObjectResult)result).Value as IEnumerable<UserDto>;
      Assert.NotNull(resultValue);
      Assert.True(resultValue.Count() == 0);
    }
  }
}