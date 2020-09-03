using System;

namespace API.Base.Core.Models
{
    public class ServiceResponse<TData> : BaseResponse
    {
        public TData Data { get; set; }
        public double Ellapsed { get; set; }   
        
        public ServiceResponse(TData data)
        {
            Data = data;
        }
        
        public ServiceResponse()
        {
        }
    }
}