using System;
using System.Collections.Generic;
using Api.Domain.Dtos.User;

namespace Api.Service.Test.User
{
  public class UserTest
  {
    public static string Name { get; set; }
    public static string Email { get; set; }
    public static string Phone { get; set; }
    public static string Sex { get; set; }

    public static string NameUpdate { get; set; }
    public static string EmailUpdate { get; set; }
    public static string PhoneUpdate { get; set; }
    public static string SexUpdate { get; set; }

    public static Guid Id { get; set; }

    public List<UserDto> listUserDto = new List<UserDto>();
    public UserDto userDto;
    public UserDtoCreate userDtoCreate;
    public UserDtoCreateResult userDtoCreateResult;
    public UserDtoUpdate userDtoUpdate;
    public UserDtoUpdateResult userDtoUpdateResult;

    public UserTest()
    {
      Id = Guid.NewGuid();
      Name = Faker.Name.First();
      Email = Faker.Internet.Email();
      Phone = "3333-8888";
      Sex = "M";
      NameUpdate = Faker.Name.First();
      EmailUpdate = Faker.Internet.Email();
      PhoneUpdate = "3333-8888";
      SexUpdate = "M";

      for (int i = 0; i < 10; i++)
      {
        listUserDto.Add(new UserDto()
        {
          Id = Guid.NewGuid(),
          Name = Faker.Name.First(),
          Email = Faker.Internet.Email(),
          Phone = "3333-8888",
          Sex = "M"
        });
      }

      userDto = new UserDto
      {
        Id = Id,
        Name = Name,
        Email = Email,
        Phone = Phone,
        Sex = Sex
      };

      userDtoCreate = new UserDtoCreate
      {
        Name = Name,
        Email = Email,
        Phone = Phone,
        Sex = Sex
      };

      userDtoCreateResult = new UserDtoCreateResult
      {
        Id = Id,
        Name = Name,
        Email = Email,
        Phone = Phone,
        Sex = Sex,
        CreateAt = DateTime.UtcNow
      };

      userDtoUpdate = new UserDtoUpdate
      {
        Name = NameUpdate,
        Email = EmailUpdate,
        Phone = PhoneUpdate,
        Sex = SexUpdate
      };

      userDtoUpdateResult = new UserDtoUpdateResult
      {
        Id = Id,
        Name = NameUpdate,
        Email = EmailUpdate,
        Phone = PhoneUpdate,
        Sex = SexUpdate,
        UpdateAt = DateTime.UtcNow
      };
    }
  }
}