using System.Net;
using System.Text;

namespace Task_1.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext,
                    ex,
                    HttpStatusCode.InternalServerError,
                    ex.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode httpStatusCode, string message)
        {
            _logger.LogCritical(ex.Message);
            _logger.LogCritical(ex.StackTrace);

            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(message));
        }
    }
}