namespace API.Base.Core.Settings
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public DefaultApiVersion DefaultApiVersion { get; set; }
        public Swagger Swagger { get; set; }
    }
}