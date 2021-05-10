using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
  public class UserMapper : BaseTestService
  {
    [Fact(DisplayName = "É possível mapear os models")]
    public void Its_Possible_Mapper_Models()
    {
      var model = new UserModel
      {
        Id = Guid.NewGuid(),
        Name = Faker.Name.FullName(),
        Email = Faker.Internet.Email(),
        Phone = "3333-4444",
        Sex = "M",
        CreateAt = DateTime.UtcNow,
        UpdateAt = DateTime.UtcNow
      };

      var listEntity = new List<UserEntity>();
      for (int i = 0; i < 5; i++)
      {
        listEntity.Add(new UserEntity
        {
          Id = Guid.NewGuid(),
          Name = Faker.Name.FullName(),
          Email = Faker.Internet.Email(),
          Phone = "3333-4444",
          Sex = "M",
          CreateAt = DateTime.UtcNow,
          UpdateAt = DateTime.UtcNow
        });
      }

      // Model to Entity
      var dtoToEntity = Mapper.Map<UserEntity>(model);
      Assert.Equal(dtoToEntity.Id, model.Id);
      Assert.Equal(dtoToEntity.Name, model.Name);
      Assert.Equal(dtoToEntity.Email, model.Email);
      Assert.Equal(dtoToEntity.Phone, model.Phone);
      Assert.Equal(dtoToEntity.Sex, model.Sex);
      Assert.Equal(dtoToEntity.CreateAt, model.CreateAt);
      Assert.Equal(dtoToEntity.UpdateAt, model.UpdateAt);

      // Entity to Dto
      var usrDto = Mapper.Map<UserDto>(dtoToEntity);
      Assert.Equal(usrDto.Id, dtoToEntity.Id);
      Assert.Equal(usrDto.Name, dtoToEntity.Name);
      Assert.Equal(usrDto.Email, dtoToEntity.Email);
      Assert.Equal(usrDto.Phone, dtoToEntity.Phone);
      Assert.Equal(usrDto.Sex, dtoToEntity.Sex);
      Assert.Equal(usrDto.CreateAt, dtoToEntity.CreateAt);

      // Enumerable Entity to Enumerable Dto
      var listDto = Mapper.Map<IEnumerable<UserDto>>(listEntity);
      Assert.True(listDto.Count() == 5);


    }

  }
}