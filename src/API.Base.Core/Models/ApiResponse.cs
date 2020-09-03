namespace API.Base.Core.Models
{
    public class ApiResponse<TData> : BaseResponse
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