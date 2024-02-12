using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Task_1.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context,
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
            
            ProblemDetails problem = new() 
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server error",
                Title = "Server error",
                Detail = String.Format("Error : {0}", ex.Message)
            };

            string json = JsonSerializer.Serialize(problem);

            await context.Response.WriteAsync(json);

            context.Response.ContentType = "application/json";
        }
    }
}