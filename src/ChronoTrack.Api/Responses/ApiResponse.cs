namespace ChronoTrack.Api.Responses
{
    public sealed class ApiResponse<T>
    {
        private ApiResponse(
            bool success,
            string? message,
            T? data,
            IReadOnlyCollection<ApiError> errors)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }

        public bool Success { get; }
        public string? Message { get; }
        public T? Data { get; }
        public IReadOnlyCollection<ApiError> Errors { get; }

        public static ApiResponse<T> Ok(T data)
        {
            return new ApiResponse<T>(
                true,
                null,
                data,
                Array.Empty<ApiError>());
        }

        public static ApiResponse<T> Fail(
            string message)
        {
            return new ApiResponse<T>(
                false,
                message,
                default,
                Array.Empty<ApiError>());
        }

        public static ApiResponse<T> Fail(
            string message,
            IReadOnlyCollection<ApiError> errors)
        {
            return new ApiResponse<T>(
                false,
                message,
                default,
                errors);
        }
    }
}