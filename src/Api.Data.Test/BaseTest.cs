namespace Api.Data.Test;

public abstract class BaseTest

    public BaseTest()
    {

    }
}

public class DbTeste : IDisposable
{

    private string databaeName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";

    public ServiceProvider ServiceProvider { get; private set; }

    public void Dispose()
    {

    }

}
