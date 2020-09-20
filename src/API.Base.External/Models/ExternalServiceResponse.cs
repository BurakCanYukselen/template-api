namespace API.Base.External.Models
{
    public class ExternalServiceResponse<TData>
    {
        public TData Data { get; set; }
        public bool IsSuccess { get; set; }
    }
}