namespace API.Base.Core.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
    }

    public class ApiResponse<TData> : ApiResponse
    {
        public TData Result { get; }

        public ApiResponse(TData result)
        {
            Result = result;
        }

        public ApiResponse()
        { 
        }
    }
}