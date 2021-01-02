using System;
using System.Linq;
using API.Base.Core.Models;
using FluentValidation;

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
                    error.AddErrorMessage(innerException.Message);

                innerException = innerException.InnerException;
            }

            return new ApiResponse<ErrorModel>(error) {Success = false};
        }

        public static ApiResponse<ErrorModel> ToApiResponse(this ValidationException exception)
        {
            var error = new ErrorModel() {StackTrace = exception.StackTrace};

            if (exception.Errors.Any())
                foreach (var validationError in exception.Errors)
                {
                    error.AddErrorMessage(validationError.ErrorMessage);
                }
            else
                return ((Exception) exception).ToApiResponse();

            return new ApiResponse<ErrorModel>(error) {Success = false};
        }
    }
}