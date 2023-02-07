using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test;

public abstract class BaseTest
{

    public BaseTest()
    {

    }
}

public class DbTeste : IDisposable
{

    private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";

    public ServiceProvider ServiceProvider { get; private set; }

    public DbTeste()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<MyContext>(o =>
            o.UseSqlServer($"Server=localhost;Database={dataBaseName};User ID=sa;Password=senha@1234;Encrypt=False"), ServiceLifetime.Transient
        );

        ServiceProvider = serviceCollection.BuildServiceProvider();
        using (var context = ServiceProvider.GetService<MyContext>())
        {
            context!.Database.EnsureCreated();
        }
    }

    public void Dispose()
    {
        using (var context = ServiceProvider.GetService<MyContext>())
        {
            context!.Database.EnsureDeleted();
        }
    }

}
