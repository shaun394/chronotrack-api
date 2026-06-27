using ChronoTrack.Api.Common.Responses;
using ChronoTrack.Api.Common.Responses.Common;
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

            object response = ex is ValidationException validationException
                ? BuildValidationResponse(validationException)
                : BuildErrorResponse(
                    (int)statusCode,
                    ex.Message);

            await httpContext.Response.WriteAsJsonAsync(response);
        }

        private static ApiResponse<object> BuildValidationResponse(
            ValidationException validationException)
        {
            var errors = validationException.Errors
                .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
                .ToList();

            var exceptionResponse = new ExceptionResponse(
                StatusCodes.Status400BadRequest,
                "Validation failed.",
                null,
                errors);

            return ApiResponse<object>.ErrorResponse(
                StatusCodes.Status400BadRequest,
                "Validation failed.",
                exceptionResponse);
        }

        private static ApiResponse<object> BuildErrorResponse(
            int statusCode,
            string message)
        {
            var exceptionResponse = new ExceptionResponse(
                statusCode,
                message,
                null,
                null);

            return ApiResponse<object>.ErrorResponse(
                statusCode,
                message,
                exceptionResponse);
        }
    }
}