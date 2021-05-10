using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.User
{
  public class RequestUser : BaseIntegration
  {
    private string _name { get; set; }
    private string _email { get; set; }

    [Fact]
    public async Task E_Possivel_Realizar_Crud_Usuario()
    {
      await AdicionarToken();
      _name = Faker.Name.First();
      _email = Faker.Internet.Email();

      var userDto = new UserDtoCreate()
      {
        Name = _name,
        Email = _email,
        Phone = "3456-6789",
        Sex = "M"
      };

      try
      {
        var response = await PostJsonAsync(userDto, $"{hostApi}users", client);
        var postResult = await response.Content.ReadAsStringAsync();
        var registerPost = JsonConvert.DeserializeObject<UserDtoCreateResult>(postResult);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(_name, registerPost.Name);
        Assert.Equal(_email, registerPost.Email);
        Assert.NotNull(registerPost.Id);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }

    }
  }
}