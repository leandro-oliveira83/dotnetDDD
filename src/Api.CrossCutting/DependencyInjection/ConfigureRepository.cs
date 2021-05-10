using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api.CrossCutting.DependencyInjection
{
  public class ConfigureRepository
  {
    public static void ConfigureDependencieRepository(IServiceCollection serviceCollection)
    {
      serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
      serviceCollection.AddScoped<IUserRepository, UserImplementation>();

      serviceCollection.AddDbContext<MyContext>(
          options => options.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION"))
      );
    }
  }
}