using AuthLab.Api.Response;
using AuthLab.Application.Exceptions;
using System.Net;

namespace AuthLab.Api.Middlewares
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

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                ValidationException => (int)HttpStatusCode.BadRequest,

                //NotFoundException => (int)HttpStatusCode.NotFound,

                //UnauthorizedException => (int)HttpStatusCode.Unauthorized,

                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;

            ErrorResponse response = exception switch
            {
                ValidationException ex => new ErrorResponse
                {
                    Message = ex.Message,
                    Errors = ex.Errors
                },

                _ => new ErrorResponse
                {
                    Message = "An unexpected error occurred."
                }
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}