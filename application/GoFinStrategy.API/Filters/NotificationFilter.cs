using GoFinStrategy.Application.Responses;
using GoFinStrategy.CrossCutting.Shared.Interfaces;
using GoFinStrategy.Shared.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Net.Mime;

namespace GoFinStrategy.API.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly INotificationContext _notificationContext;

        public NotificationFilter(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!_notificationContext.IsValid)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;

                var notification = _notificationContext.Notifications.LastOrDefault();
                var response = JsonHelper.ToStringNS(ErrorResponse.Content(notification?.Key, notification?.Message));
                await context.HttpContext.Response.WriteAsync(response);

                return;
            }

            await next();
        }
    }
}
