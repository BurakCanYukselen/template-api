using API.Base.External.Abstract;

namespace API.Base.External
{
    public interface IExampleExternalService
    {
    }

    public class ExampleExternalService : AbstractExternalService, IExampleExternalService
    {
        public ExampleExternalService(ExampleExternalServiceHttpClient client) : base(client)
        {
        }
    }

    public class ExampleExternalServiceHttpClient : AbstractHttpClient
    {
        public ExampleExternalServiceHttpClient(string url) : base(url)
        {
        }
    }
}