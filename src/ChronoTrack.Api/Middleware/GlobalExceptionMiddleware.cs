using ChronoTrack.Api.Responses;
using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Domain.Common.Exceptions;
using FluentValidation;
using System.Net;

namespace ChronoTrack.Api.Middleware
{
    public sealed class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
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
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext httpContext,
            Exception ex)
        {
            HttpStatusCode statusCode = ex switch
            {
                ValidationException => HttpStatusCode.BadRequest,
                DomainValidationException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                DuplicateEntityException => HttpStatusCode.Conflict,
                _ => HttpStatusCode.InternalServerError
            };

            if (statusCode == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
            }

            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = "application/json";

            var response = ex is ValidationException validationException
                ? BuildValidationResponse(validationException)
                : ApiResponse<object>.Fail(ex.Message);

            await httpContext.Response.WriteAsJsonAsync(response);
        }

        private static ApiResponse<object> BuildValidationResponse(
            ValidationException validationException)
        {
            var errors = validationException.Errors
                .Select(error => new ApiError
                {
                    Field = error.PropertyName,
                    Message = error.ErrorMessage
                })
                .ToList();

            return ApiResponse<object>.Fail(
                "Validation failed.",
                errors);
        }
    }
}