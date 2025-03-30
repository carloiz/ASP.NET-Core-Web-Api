using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;

namespace ASP.NetCoreWebApi.common.Middlewares
{
    public class ExceptionHandlerMiddlewares
    {
        private readonly ILogger<ExceptionHandlerMiddlewares> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddlewares(ILogger<ExceptionHandlerMiddlewares> logger,
                                          RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                // Log the exception
                logger.LogError(ex, $"{errorId} : {ex.Message}");

                // Determine the appropriate status code based on the exception type
                var statusCode = ex switch
                {
                    ArgumentNullException => HttpStatusCode.BadRequest,
                    ArgumentException => HttpStatusCode.BadRequest,
                    UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                    SecurityException => HttpStatusCode.Forbidden,
                    KeyNotFoundException => HttpStatusCode.NotFound,
                    NotSupportedException => HttpStatusCode.MethodNotAllowed,
                    InvalidOperationException => HttpStatusCode.Conflict,
                    ValidationException => HttpStatusCode.UnprocessableEntity,
                    ApplicationException => HttpStatusCode.InternalServerError,
                    _ => HttpStatusCode.InternalServerError
                };

                // Return a custom error response
                httpContext.Response.StatusCode = (int)statusCode;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    TeamMessage = "Something went wrong, we are looking into resolving this.",
                    ErrorMessage = ex.Message,
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }

    }
}
