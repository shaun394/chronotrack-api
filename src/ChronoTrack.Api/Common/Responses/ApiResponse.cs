namespace ChronoTrack.Api.Common.Responses
{
    public abstract class ApiResponse
    {
        protected ApiResponse(
            bool success,
            int statusCode,
            string? message,
            object? errors = null,
            string? traceId = null)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            Errors = errors;
            TraceId = traceId;
        }

        public bool Success { get; init; }
        public int StatusCode { get; init; }
        public string? Message { get; init; }
        public object? Errors { get; init; }
        public string? TraceId { get; init; }
    }

    public sealed class ApiResponse<T> : ApiResponse
    {
        private ApiResponse(
            bool success,
            int statusCode,
            string? message,
            T? data,
            object? errors,
            string? traceId)
            : base(success, statusCode, message, errors, traceId)
        {
            Data = data;
        }

        public T? Data { get; init; }

        public static ApiResponse<T> SuccessResponse(
            T data,
            int statusCode,
            string? message = null)
        {
            return new(
                success: true,
                statusCode: statusCode,
                message: message,
                data: data,
                errors: null,
                traceId: null);
        }

        public static ApiResponse<T> ErrorResponse(
            int statusCode,
            string message,
            object? errors = null,
            string? traceId = null)
        {
            return new(
                success: false,
                statusCode: statusCode,
                message: message,
                data: default,
                errors: errors,
                traceId: traceId);
        }
    }
}
