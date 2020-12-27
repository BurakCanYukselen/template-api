using System;
using API.Base.Core.Models;

namespace API.Base.Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static ApiResponse<ErrorModel> ToApiResponse(this Exception exception)
        {
            var error = new ErrorModel() {StackTrace = exception.StackTrace};

            if (!string.IsNullOrEmpty(exception.Message))
                error.AddErrorMessage(exception.Message);

            var innerException = exception.InnerException;
            while (innerException != null)
            {
                if (!string.IsNullOrEmpty(innerException.Message))
                    error.AddErrorMessage(exception.Message);

                innerException = innerException.InnerException;
            }

            return new ApiResponse<ErrorModel>(error) {Success = false};
        }
    }
}