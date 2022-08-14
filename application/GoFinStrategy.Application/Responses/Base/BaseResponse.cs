using GoFinStrategy.Application.Enums;
using System.ComponentModel;

namespace GoFinStrategy.Application.Responses
{
    public class BaseResponse
    {
        public ResponseStatus Status { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

        [Description("Only used by Serializer")]
        public BaseResponse() { }

        public BaseResponse(ResponseStatus status, string errorCode, string message, object result)
        {
            Status = status;
            Message = message;
            Result = result;
            ErrorCode = errorCode;
        }

        protected void Success<T>(string message, T result)
        {
            Status = ResponseStatus.Success;
            Message = message;
            Result = result;
        }

        protected void Success(string message = null)
        {
            Status = ResponseStatus.Success;
            Message = message;
            Result = null;
        }

        protected void Warning<T>(string errorCode, string message, T result)
        {
            Status = ResponseStatus.Warning;
            ErrorCode = errorCode;
            Message = message;
            Result = result;
        }

        protected void Warning(string errorCode, string message = null)
        {
            Status = ResponseStatus.Warning;
            ErrorCode = errorCode;
            Message = message;
            Result = null;
        }

        protected void Error<T>(string errorCode, string message, T result)
        {
            Status = ResponseStatus.Error;
            ErrorCode = errorCode;
            Message = message;
            Result = result;
        }

        protected void Error(string errorCode, string message = null)
        {
            Status = ResponseStatus.Error;
            ErrorCode = errorCode;
            Message = message;
            Result = null;
        }
    }
}
