using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
  public class UserFullCrud : BaseTest, IClassFixture<DbTest>
  {
    private ServiceProvider _serviceProvide;

    public UserFullCrud(DbTest dbTeste) => _serviceProvide = dbTeste.ServiceProvider;

    [Fact(DisplayName = "user CRUD")]
    [Trait("CRUD", "UserEntity")]
    public async Task FullTestCrudUser()
    {
      using (var context = _serviceProvide.GetService<MyContext>())
      {
        UserImplementation _repository = new UserImplementation(context);
        UserEntity _entity = new UserEntity
        {
          Email = "teste@gmail.com", //Faker.Internet.Email(),
          Name = "teste", //Faker.Name.FullName(),
          Phone = "333-4444", //Faker.Phone.Number(),
          Sex = "m"
        };

        var _createRegister = await _repository.InsertAsync(_entity);
        Assert.NotNull(_createRegister);
        Assert.False(_createRegister.Id == Guid.Empty);
        Assert.Equal(_createRegister.Email, _entity.Email);
        Assert.Equal(_createRegister.Name, _entity.Name);
        Assert.Equal(_createRegister.Phone, _entity.Phone);
        Assert.Equal(_createRegister.Sex, _entity.Sex);

        _entity.Name = "Novo";//Faker.Name.First();
        var _updateRegister = await _repository.UpdateAsync(_entity);
        Assert.NotNull(_createRegister);
        Assert.Equal(_updateRegister.Email, _entity.Email);
        Assert.Equal(_updateRegister.Name, _entity.Name);
        Assert.Equal(_updateRegister.Phone, _entity.Phone);
        Assert.Equal(_updateRegister.Sex, _entity.Sex);

        var _existRegister = await _repository.ExistAsync(_updateRegister.Id);
        Assert.True(_existRegister);

        var _getRegister = await _repository.SelectAsync(_updateRegister.Id);
        Assert.NotNull(_getRegister);
        Assert.Equal(_getRegister.Email, _updateRegister.Email);
        Assert.Equal(_getRegister.Name, _updateRegister.Name);
        Assert.Equal(_getRegister.Phone, _updateRegister.Phone);
        Assert.Equal(_getRegister.Sex, _updateRegister.Sex);

        var _getAllRegister = await _repository.SelectAsync();
        Assert.NotNull(_getAllRegister);
        Assert.True(_getAllRegister.Count() > 0);

        var _deleteRegister = await _repository.DeleteAsync(_updateRegister.Id);
        Assert.True(_deleteRegister);

        var _defaultRegister = await _repository.FindByLogin("admin@gmail.com");
        Assert.NotNull(_defaultRegister);
        Assert.Equal("admin@gmail.com", _defaultRegister.Email);
        Assert.Equal("Administrador", _defaultRegister.Name);
      }
    }
  }
}