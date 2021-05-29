namespace API.Base.Core.Models
{
    public class ApiResponse<TData> : BaseResponse
    {
        public ApiResponse(TData result)
        {
            Result = result;
        }

        public ApiResponse()
        {
        }

        public TData Result { get; }
    }
}