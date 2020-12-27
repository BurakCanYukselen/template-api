using API.Base.Core.Models;

namespace API.Base.Core.Extensions
{
    public static class ObjectExtension
    {
        public static ApiResponse<object> ToApiResponse(this object source, bool success)
        {
            return new ApiResponse<object>(source) {Success = success};
        }
    }
}