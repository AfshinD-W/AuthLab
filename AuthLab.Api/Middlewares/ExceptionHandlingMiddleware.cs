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

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                ValidationException => (int)HttpStatusCode.BadRequest,

                BusinessException => (int)HttpStatusCode.Conflict,

                NotFoundException => (int)HttpStatusCode.NotFound,

                UnauthorizedException => (int)HttpStatusCode.Unauthorized,

                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;

            ApiResponse<object> response = new()
            {
                Success = false,
                Message = statusCode == (int)HttpStatusCode.InternalServerError ? "An unexpected error occurred." : exception.Message
            };

            if (exception is ValidationException validation)
            {
                response.Errors = validation.Errors;
            }
            else if (exception is BusinessException business)
            {
                response.Errors = business.Errors;
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}