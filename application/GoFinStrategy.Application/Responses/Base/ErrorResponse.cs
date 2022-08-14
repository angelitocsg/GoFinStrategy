namespace GoFinStrategy.Application.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public static ErrorResponse Content<T>(string errorCode, string message, T result)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            errorResponse.Error(errorCode, message, result);

            return errorResponse;
        }

        public static ErrorResponse Content(string errorCode, string message = null)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            errorResponse.Error(errorCode, message);

            return errorResponse;
        }
    }
}
