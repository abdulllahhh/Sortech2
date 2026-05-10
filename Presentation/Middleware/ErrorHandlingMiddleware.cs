using Application.Exeptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Presentation.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            object response;

            if (exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
                response = new { error = exception.Message };
            }
            else if (exception is ValidationException validationException)
            {
                code = HttpStatusCode.BadRequest;

                response = new
                {
                    error = "Validation failed",
                    details = validationException.Errors.Select(e => e.ErrorMessage)
                };
            }
            else
            {
                response = new
                {
                    error = "An unexpected error occurred."
                };
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            var result = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(result, context.RequestAborted);
        }
    }
}