using GoFinStrategy.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace GoFinStrategy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger _logger;

        protected BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected ErrorResponse? ModelStateErrors()
        {
            if (ModelState.IsValid) return null;

            var modelErrors = from values in ModelState.Values
                              from error in values.Errors
                              select error.Exception?.ToString() ?? error.ErrorMessage;

            return ErrorResponse.Content("-108", "Invalid model", modelErrors);
        }
    }
}
