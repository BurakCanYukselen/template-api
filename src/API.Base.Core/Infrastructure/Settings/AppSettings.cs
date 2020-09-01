namespace API.Base.Core.Infrastructure.Settings
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public DefaultApiVersion DefaultApiVersion { get; set; }
        public Swagger Swagger { get; set; }
    }
}