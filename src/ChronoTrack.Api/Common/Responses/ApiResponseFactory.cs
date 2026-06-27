using Microsoft.AspNetCore.Mvc;

namespace ChronoTrack.Api.Common.Responses
{
    public sealed class ApiResponseFactory
    {
        public static IActionResult Ok<T>(
            T data,
            string message = "Operation Successful.")
            => new OkObjectResult(
                ApiResponse<T>.SuccessResponse(
                    data,
                    StatusCodes.Status200OK,
                    message));

        public static IActionResult Created<T>(
            string actionName,
            string controllerName,
            object routeValues,
            T data,
            string message = "Resource created successfully.")
            => new CreatedAtActionResult(
                actionName,
                controllerName,
                routeValues,
                ApiResponse<T>.SuccessResponse(
                    data,
                    StatusCodes.Status201Created,
                    message));

        public static IActionResult NoContent()
            => new NoContentResult();

        public static IActionResult BadRequest(
            string message,
            object? errors = null)
            => new BadRequestObjectResult(
                ApiResponse<object>.ErrorResponse(
                    StatusCodes.Status404NotFound,
                    message));

        public static IActionResult NotFound(
            string message = "Record not found.")
            => new NotFoundObjectResult(
                ApiResponse<object>.ErrorResponse(
                    StatusCodes.Status404NotFound,
                    message));

        public static IActionResult Conflict(
            string message = "A conflict occurred.")
            => new ConflictObjectResult(
                ApiResponse<object>.ErrorResponse(
                    StatusCodes.Status409Conflict,
                    message));

        public static IActionResult Unauthorized(
            string message = "Unauthorized access.")
            => new ObjectResult(
                ApiResponse<object>.ErrorResponse(
                    StatusCodes.Status401Unauthorized,
                    message))
            { StatusCode = StatusCodes.Status401Unauthorized };

        public static IActionResult Forbidden(
            string message = "Forbidden access.")
            => new ObjectResult(
                ApiResponse<object>.ErrorResponse(
                    StatusCodes.Status403Forbidden,
                    message))
            { StatusCode = StatusCodes.Status403Forbidden };

        public static IActionResult ServerError(
            string message = "An unexpected error occurred.")
            => new ObjectResult(
                ApiResponse<object>.ErrorResponse(
                    StatusCodes.Status500InternalServerError,
                    message))
            { StatusCode = StatusCodes.Status500InternalServerError };
    }
}