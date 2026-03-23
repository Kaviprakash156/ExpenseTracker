using System.Net;
using System.Text.Json;

namespace ExpenseTracker.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;

            response.ContentType = "application/json";

            // Default
            var statusCode = (int)HttpStatusCode.InternalServerError;

            // Custom handling
            if (ex.Message.Contains("not found"))
            {
                statusCode = (int)HttpStatusCode.NotFound;
            }

            response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new
            {
                statusCode = statusCode,
                message = ex.Message
            });

            return response.WriteAsync(result);
        }
    }
}