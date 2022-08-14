namespace GoFinStrategy.Application.Responses
{
    public class SuccessResponse : BaseResponse
    {
        public static SuccessResponse Content<T>(string message, T result)
        {
            SuccessResponse successResponse = new SuccessResponse();
            successResponse.Success(message, result);

            return successResponse;
        }

        public static SuccessResponse Content<T>(T result)
        {
            SuccessResponse successResponse = new SuccessResponse();
            successResponse.Success(string.Empty, result);

            return successResponse;
        }

        public static SuccessResponse Content(string message = null)
        {
            SuccessResponse successResponse = new SuccessResponse();
            successResponse.Success(message);

            return successResponse;
        }
    }
}
