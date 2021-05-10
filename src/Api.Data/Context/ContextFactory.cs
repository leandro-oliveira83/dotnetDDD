using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
  public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
  {
    public MyContext CreateDbContext(string[] args)
    {
        //using for create my migrations
        var connectionString = "Server=127.0.0.1;Port=3306;Database=dbSampleDDD;Uid=root;Pwd=102030";
        var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
        optionsBuilder.UseMySql (connectionString);

        return new MyContext(optionsBuilder.Options);
    }
  }
}