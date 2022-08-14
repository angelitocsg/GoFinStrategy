namespace GoFinStrategy.API.Middlewares
{
    public class ModelValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ModelValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var requestUrl = httpContext.Request.Path;

            // logic here

            await _next(httpContext);
        }
    }

    public static class ModelValidationExtensions
    {
        public static IApplicationBuilder UseModelValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ModelValidationMiddleware>();
        }
    }
}
