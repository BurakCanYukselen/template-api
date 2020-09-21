namespace API.Base.Core.Settings
{
    public class AppSettings
    {
        public string ApplicationSecret { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public ExternalServices ExternalServices { get; set; }
        public DefaultApiVersion DefaultApiVersion { get; set; }
        public Swagger Swagger { get; set; }
    }
}