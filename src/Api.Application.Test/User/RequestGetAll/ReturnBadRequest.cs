using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.RequestGetAll
{
  public class ReturnBadRequest
  {
    private UsersController _controller;

    [Fact(DisplayName = "É possível teste bad request.")]
    public async Task Test_Execute_Controller_BadRequest()
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
      _controller.ModelState.AddModelError("Id", "Formato Inválido");

      var result = await _controller.Get(Guid.NewGuid());
      Assert.True(result is BadRequestObjectResult);
    }
  }
}