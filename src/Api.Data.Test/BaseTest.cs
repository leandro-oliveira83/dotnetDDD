
using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test
{
  public abstract class BaseTest
  {
    public BaseTest()
    {

    }
  }

  /// <summary>
  /// Responsible for creating database name and context
  /// </summary>
  public class DbTest : IDisposable
  {
    // generate dynamic database name
    private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
    public ServiceProvider ServiceProvider { get; private set; }

    public DbTest()
    {
      var serviceCollection = new ServiceCollection();
      serviceCollection.AddDbContext<MyContext>(o =>
        o.UseMySql($"Persist Security Info=True;Server=127.0.0.1;Port=3306;Database={dataBaseName};Uid=root;Pwd=102030"),
          ServiceLifetime.Transient // Open one connect for each request
      );
      // Build connection string
      ServiceProvider = serviceCollection.BuildServiceProvider();
      // Create temporary database Test
      using (var context = ServiceProvider.GetService<MyContext>())
        context.Database.EnsureCreated();
    }
    public void Dispose()
    {
      // Delete temporary database Test
      using (var context = ServiceProvider.GetService<MyContext>())
        context.Database.EnsureDeleted();
    }
  }
}