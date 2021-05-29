namespace API.Base.Core.Models
{
    public class ServiceResponse<TData> : BaseResponse
    {
        public ServiceResponse(TData data)
        {
            Data = data;
        }

        public ServiceResponse()
        {
        }

        public TData Data { get; set; }
        public double Ellapsed { get; set; }
    }
}